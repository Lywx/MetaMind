namespace MetaMind.Engine.Entities.Nodes
{
    using Actions;
    using NLog;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// There are 2 different types of callbacks (selectors):
    /// 
    /// Update selector: the 'update' selector will be called every frame. You
    /// can customize the priority.
    /// 
    /// Custom selector: A custom selector will be called every frame, or with a
    /// custom interval of time
    /// 
    /// The 'custom selectors' should be avoided when possible. It is faster,
    /// and consumes less memory to use the 'update selector'.
    /// </summary>
    public class CCScheduler
    {
        #region Logger

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Update Data

        private CustomItemHash currentTarget;

        private bool currentTargetSalvaged;

        private bool updateHashLocked;

        private static ICCUpdatable[] updateItemsDynamic = new ICCUpdatable[128];

        /// <summary>
        /// List of items with priority equals 0.
        /// </summary>
        /// <remarks>
        /// Hash used to fetch quickly the list entries for pause, delete, etc.
        /// </remarks>
        private readonly LinkedList<UpdateItem> updateZeroItems = new LinkedList<UpdateItem>();

        /// <summary>
        /// List of entities with priority smaller than 0. The list is sorted by priority.
        /// </summary>
        private readonly LinkedList<UpdateItem> updateNegativeItems = new LinkedList<UpdateItem>();

        /// <summary>
        /// List of entities with priority large than 0. The list is sorted by priority. 
        /// </summary>
        private readonly LinkedList<UpdateItem> updatePositiveItems = new LinkedList<UpdateItem>();

        /// <summary>
        /// Hash of entities in update item list. This is used to quickly access
        /// the item without searching in LinkedList.
        /// </summary>
        private readonly Dictionary<ICCUpdatable, UpdateItemHash> updateHashes = new Dictionary<ICCUpdatable, UpdateItemHash>();

        /// <summary>
        /// Hash of entities in custom item list. This is used to quickly access
        /// the item without searching in LinkedList.
        /// </summary>
        private readonly Dictionary<ICCUpdatable, CustomItemHash> customHashes = new Dictionary<ICCUpdatable, CustomItemHash>();

        private static CustomItemHash[] customItemsDynamic = new CustomItemHash[128];

        #endregion

        #region Time Data

        public double TimeScale { get; set; } = 1.0;

        #endregion

        #region Manager Data

        public MMActionManager ActionManager { get; }

        /// <summary>
        /// Gets a value indicating whether the ActionManager is active.
        /// </summary>
        /// <remarks>
        /// The ActionManager can be stopped from processing actions by calling UnscheduleAll() method.
        /// </remarks>
        public bool IsActionManagerActive
        {
            get
            {
                if (this.ActionManager != null)
                {
                    var target = this.ActionManager;

                    LinkedListNode<UpdateItem> next;

                    for (var node = this.updateNegativeItems.First; node != null; node = next)
                    {
                        next = node.Next;

                        if (node.Value.Target == target &&
                           !node.Value.IsDeleting)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
        }

        #endregion

        #region Constructors

        internal CCScheduler(MMActionManager actionManager)
        {
            if (actionManager == null)
            {
                throw new ArgumentNullException(nameof(actionManager));
            }

            this.ActionManager = actionManager;
        }

        #endregion Constructors

        #region Update

        internal void Update(float dt)
        {
            this.updateHashLocked = true;

            try
            {
                dt *= (float)this.TimeScale;

                // First update entries with lower priority value
                this.UpdateList(dt, this.updateNegativeItems);
                this.UpdateList(dt, this.updateZeroItems);
                this.UpdateList(dt, this.updatePositiveItems);

                // Iterate over all the custom selectors
                var count = this.customHashes.Count;
                if (count > 0)
                {
                    // Dynamic array resizing when necessary
                    if (updateItemsDynamic.Length < count)
                    {
                        updateItemsDynamic =
                            new ICCUpdatable[updateItemsDynamic.Length * 2];
                    }

                    this.customHashes.Keys.CopyTo(updateItemsDynamic, 0);

                    for (var i = 0; i < count; i++)
                    {
                        var key = updateItemsDynamic[i];

                        if (!this.customHashes.ContainsKey(key))
                        {
                            continue;
                        }

                        var target = this.customHashes[key];

                        this.currentTarget = target;
                        this.currentTargetSalvaged = false;

                        if (!this.currentTarget.Paused)
                        {
                            // The 'timers' array may change while inside this loop
                            for (target.TimerIndex = 0;
                                target.TimerIndex < target.Timers.Count;
                                ++target.TimerIndex)
                            {
                                target.CurrentTimer = target.Timers[target.TimerIndex];
                                if (target.CurrentTimer != null)
                                {
                                    target.CurrentTimerSalvaged = false;
                                    target.CurrentTimer.Update(dt);
                                    target.CurrentTimer = null;
                                }
                            }
                        }

                        // Only delete currentTarget if no actions were
                        // scheduled during the cycle (issue #481)
                        if (this.currentTargetSalvaged
                            && this.currentTarget.Timers.Count == 0)
                        {
                            this.RemoveCustomSelector(this.currentTarget);
                        }
                    }
                }

                // Delete all updates that are marked for deletion
                this.UpdateListRemoval(this.updateZeroItems);
                this.UpdateListRemoval(this.updateNegativeItems);
                this.UpdateListRemoval(this.updatePositiveItems);
            }
            finally
            {
                this.updateHashLocked = false;
                this.currentTarget = null;
            }
        }

        private void UpdateList(float dt, LinkedList<UpdateItem> updateList)
        {
            LinkedListNode<UpdateItem> nextNode;

            for (var node = updateList.First; node != null; node = nextNode)
            {
                nextNode = node.Next;
                var nextEntry = node.Value;

                if (!nextEntry.IsPaused
                    && !nextEntry.IsDeleting)
                {
                    nextEntry.Target.Update(dt);
                }
            }
        }

        private void UpdateListRemoval(LinkedList<UpdateItem> updateList)
        {
            LinkedListNode<UpdateItem> nextNode;

            for (var node = updateList.First; node != null; node = nextNode)
            {
                nextNode = node.Next;
                var nextEntry = node.Value;

                if (nextEntry.IsDeleting)
                {
                    updateList.Remove(node);
                    this.RemoveUpdateSelector(nextEntry);
                }
            }
        }

        #endregion

        #region Add Operations

        private void AddUpdateSelector(
            LinkedList<UpdateItem> list,
            ICCUpdatable target,
            bool paused)
        {
            var item = new UpdateItem
            {
                Target          = target,
                IsPaused          = paused,
                IsDeleting = false,
            };

            list.AddLast(item);

            this.AddUpdateSelectorHash(list, target, item);
        }

        private void AddUpdateSelectorHash(
            LinkedList<UpdateItem> list,
            ICCUpdatable target,
            UpdateItem item)
        {
            var itemHash = new UpdateItemHash
            {
                Target = target,
                List = list,
                Item = item
            };

            this.updateHashes.Add(target, itemHash);
        }

        private void InsertUpdateSelector(
            LinkedList<UpdateItem> list,
            ICCUpdatable target,
            int priority,
            bool paused)
        {
            var item = new UpdateItem
            {
                Target = target,
                Priority = priority,
                IsPaused = paused,
                IsDeleting = false
            };

            if (list.First == null)
            {
                list.AddFirst(item);
            }
            else
            {
                var added = false;

                for (var node = list.First; node != null; node = node.Next)
                {
                    if (priority < node.Value.Priority)
                    {
                        list.AddBefore(node, item);
                        added = true;
                        break;
                    }
                }

                if (!added)
                {
                    list.AddLast(item);
                }
            }

            this.AddUpdateSelectorHash(list, target, item);
        }

        /// <summary>
        /// Resurrect the given selector that is about to be deleted.
        /// </summary>
        /// <param name="updateSelector"></param>
        private void ResurrectUpdateSelector(UpdateItemHash updateSelector)
        {
            if (!updateSelector.Item.IsDeleting)
            {
                throw new InvalidOperationException();
            }

            // Resurrect the selector.
            updateSelector.Item.IsDeleting = false;
        }

        #endregion

        #region Remove Operations

        private void RemoveCustomSelector(CustomItemHash itemHash)
        {
            this.customHashes.Remove(itemHash.Target);

            itemHash.Timers.Clear();
            itemHash.Target = null;
        }

        private void RemoveUpdateSelector(UpdateItem item)
        {
            UpdateItemHash itemHash;

            if (this.TryGetUpdateSelector(item.Target, out itemHash))
            {
                // Remove from list
                itemHash.List.Remove(item);
                itemHash.Item = null;

                // Remove from hash
                this.updateHashes.Remove(item.Target);
                itemHash.Target = null;
            }
        }


        #endregion

        #region Get Operations

        private bool TryGetCustomSelector(ICCUpdatable target, out CustomItemHash itemHash)
        {
            return this.customHashes.TryGetValue(target, out itemHash);
        }

        private bool TryGetUpdateSelector(ICCUpdatable target, out UpdateItemHash itemHash)
        {
            return this.updateHashes.TryGetValue(target, out itemHash);
        }

        #endregion

        #region Schedule Operations

        /// <summary>
        /// Schedule a custom selector.
        /// </summary>
        /// <remarks>
        /// The scheduled method will be called every 'interval' seconds.
        /// If paused is YES, then it won't be called until it is resumed.
        /// If 'interval' is 0, it will be called every frame, but if so, it's recommended to use 'scheduleUpdateForTarget:' instead.
        /// If the selector is already scheduled, then only the interval parameter will be updated without re-scheduling it again.
        /// repeat let the action be repeated repeat + 1 times, use RepeatForever to let the action run continuously
        /// delay is the amount of time the action will wait before it'll start
        /// @since v0.99.3, repeat and delay added in v1.1
        /// </remarks>
        public void Schedule(
            Action<float> selector,
            ICCUpdatable target,
            float interval,
            uint repeat,
            float delay,
            bool paused)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            lock (this.customHashes)
            {
                CustomItemHash customSelector;

                if (!this.TryGetCustomSelector(target, out customSelector))
                {
                    customSelector = new CustomItemHash
                    {
                        Target = target,
                        Paused = paused
                    };

                    this.customHashes[target] = customSelector;

                    // Is this the 1st element ? Then set the pause level to all the selectors of this target
                }
                else
                {
                    if (customSelector != null)
                    {
                        Debug.Assert(customSelector.Paused == paused, "CCScheduler.Schedule: All are paused");
                    }
                }

                if (customSelector != null)
                {
                    if (customSelector.Timers == null)
                    {
                        customSelector.Timers = new List<CCTimer>();
                    }
                    else
                    {
                        var timers = customSelector.Timers.ToArray();
                        foreach (var timer in timers)
                        {
                            if (timer == null)
                            {
                                continue;
                            }

                            if (selector == timer.Selector)
                            {
                                logger.Warn($"Selector already scheduled. Updating interval from: {timer.Interval} to {interval}");

                                timer.Interval = interval;

                                return;
                            }
                        }
                    }

                    customSelector.Timers.Add(new CCTimer(this, target, selector, interval, repeat, delay));
                }
            }
        }

        /// <summary>
        /// Schedules the 'Update' selector for a given target with a given
        /// priority. The 'Update' selector will be called every frame. The
        /// 'Update' selector will be called every frame.
        /// </summary>
        /// <param name="priority">
        /// The lower the priority, the earlier it is called.
        /// </param>
        public void Schedule(ICCUpdatable target, int priority, bool paused)
        {
            UpdateItemHash updateSelector;

            // When has been added before but has not been removed yet.
            if (this.updateHashes.TryGetValue(target, out updateSelector))
            {
                // The only possibility for this scenario is that the item is
                // about to be deleted.
                Debug.Assert(updateSelector.Item.IsDeleting);

                this.ResurrectUpdateSelector(updateSelector);

                return;
            }

            // Most of the updates are going to be 0, that's way there
            // is an special list for updates with priority 0.
            if (priority == 0)
            {
                this.AddUpdateSelector(this.updateZeroItems, target, paused);
            }
            else if (priority < 0)
            {
                this.InsertUpdateSelector(this.updateNegativeItems, target, priority, paused);
            }
            else
            {
                this.InsertUpdateSelector(this.updatePositiveItems, target, priority, paused);
            }
        }

        /// <summary>
        /// Unschedule a selector for a given target. If you want to unschedule
        /// the "Update", use unscheudleUpdateForTarget.
        /// </summary>
        public void Unschedule(Action<float> selector, ICCUpdatable target)
        {
            // explicitly handle nil arguments when removing an object
            if (selector == null || target == null)
            {
                return;
            }

            CustomItemHash element;

            if (this.customHashes.TryGetValue(target, out element))
            {
                for (var i = 0; i < element.Timers.Count; i++)
                {
                    var timer = element.Timers[i];

                    if (selector == timer.Selector)
                    {
                        if (timer == element.CurrentTimer && (!element.CurrentTimerSalvaged))
                        {
                            element.CurrentTimerSalvaged = true;
                        }

                        element.Timers.RemoveAt(i);

                        // Update timerIndex in case we are in tick:, looping over the actions
                        if (element.TimerIndex >= i)
                        {
                            element.TimerIndex--;
                        }

                        if (element.Timers.Count == 0)
                        {
                            if (this.currentTarget == element)
                            {
                                this.currentTargetSalvaged = true;
                            }
                            else
                            {
                                this.RemoveCustomSelector(element);
                            }
                        }

                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Unschedules all selectors for a given target. This also includes the
        /// "Update" selector.
        /// </summary>
        public void Unschedule(ICCUpdatable target)
        {
            if (target == null)
            {
                return;
            }

            UpdateItemHash updateSelector;

            if (this.TryGetUpdateSelector(target, out updateSelector))
            {
                if (this.updateHashLocked)
                {
                    updateSelector.Item.IsDeleting = true;
                }
                else
                {
                    this.RemoveUpdateSelector(updateSelector.Item);
                }
            }
        }

        public void UnscheduleAll(int minPriority)
        {
            var count = this.customHashes.Values.Count;
            if (customItemsDynamic.Length < count)
            {
                customItemsDynamic = new CustomItemHash[customItemsDynamic.Length * 2];
            }

            this.customHashes.Values.CopyTo(customItemsDynamic, 0);

            for (int i = 0; i < count; i++)
            {
                // Element may be removed in unscheduleAllSelectorsForTarget
                this.UnscheduleAll(customItemsDynamic[i].Target);
            }

            // Updates selectors
            if (minPriority < 0 && this.updateNegativeItems.Count > 0)
            {
                LinkedList<UpdateItem> copy = new LinkedList<UpdateItem>(this.updateNegativeItems);
                foreach (UpdateItem entry in copy)
                {
                    if (entry.Priority >= minPriority)
                    {
                        this.UnscheduleAll(entry.Target);
                    }
                }
            }

            if (minPriority <= 0 && this.updateZeroItems.Count > 0)
            {
                LinkedList<UpdateItem> copy = new LinkedList<UpdateItem>(this.updateZeroItems);
                foreach (UpdateItem entry in copy)
                {
                    this.UnscheduleAll(entry.Target);
                }
            }

            if (this.updatePositiveItems.Count > 0)
            {
                LinkedList<UpdateItem> copy = new LinkedList<UpdateItem>(this.updatePositiveItems);
                foreach (UpdateItem entry in copy)
                {
                    if (entry.Priority >= minPriority)
                    {
                        this.UnscheduleAll(entry.Target);
                    }
                }
            }
        }

        public void UnscheduleAll(ICCUpdatable target)
        {
            if (target == null)
            {
                return;
            }

            // Custom selectors
            CustomItemHash element;

            if (this.customHashes.TryGetValue(target, out element))
            {
                if (element.Timers.Contains(element.CurrentTimer))
                {
                    element.CurrentTimerSalvaged = true;
                }

                element.Timers.Clear();

                if (this.currentTarget == element)
                {
                    this.currentTargetSalvaged = true;
                }
                else
                {
                    this.RemoveCustomSelector(element);
                }
            }

            // update selector
            this.Unschedule(target);
        }

        public void UnscheduleAll()
        {
            // This also stops ActionManger from updating which means all
            // actions are stopped as well.
            this.UnscheduleAll(CCSchedulePriority.System);
        }

        #endregion

        #region Pause Operations

        public bool IsTargetPaused(ICCUpdatable target)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            CustomItemHash customSelector;
            if (this.TryGetCustomSelector(target, out customSelector))
            {
                return customSelector.Paused;
            }

            // We should still check update selectors if target does not have
            // custom selectors
            UpdateItemHash updateSelector;
            if (this.TryGetUpdateSelector(target, out updateSelector))
            {
                return updateSelector.Item.IsPaused;
            }

            // Should never get here
            throw new InvalidOperationException();
        }

        public List<ICCUpdatable> PauseAllTargets()
        {
            return this.PauseAllTargets(int.MinValue);
        }

        /// <summary>
        /// Pause all existing selector including update selectors and custom selectors.
        /// </summary>
        /// <param name="priorityMinimalThreshold">
        /// Minimum value of priority which is used for this pausing operation
        /// to filter the existing selectors. If the selectors with priority
        /// that is higher or equal than the threshold will be paused.
        /// </param>
        /// <returns>
        /// List of paused selectors.
        /// </returns>
        private List<ICCUpdatable> PauseAllTargets(int priorityMinimalThreshold)
        {
            var pausedSelectors = new List<ICCUpdatable>();

            this.PauseCustomSelectors(pausedSelectors);
            this.PauseUpdateSelectors(pausedSelectors, priorityMinimalThreshold);

            return pausedSelectors;
        }

        public void PauseTarget(ICCUpdatable target)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            // Custom selectors
            CustomItemHash customSelector;
            if (this.TryGetCustomSelector(target, out customSelector))
            {
                customSelector.Paused = true;
            }

            // Update selectors
            UpdateItemHash updateSelector;
            if (this.TryGetUpdateSelector(target, out updateSelector))
            {
                updateSelector.Item.IsPaused = true;
            }
        }

        private void PauseCustomSelectors(List<ICCUpdatable> pausedSelectors)
        {
            foreach (var itemHash in this.customHashes.Values)
            {
                itemHash.Paused = true;

                if (!pausedSelectors.Contains(itemHash.Target))
                {
                    pausedSelectors.Add(itemHash.Target);
                }
            }
        }

        /// <param name="priorityMinimalThreshold">
        /// Minimum value of priority which is used for this pausing operation
        /// to filter the existing selectors. If the selectors with priority
        /// that is higher or equal than the threshold will be paused.
        /// </param>
        private void PauseUpdateSelectors(List<ICCUpdatable> pausedSelectors, int priorityMinimalThreshold)
        {
            Predicate<int> commonPausedPredicate =
                priority => priority >= priorityMinimalThreshold;

            var updateNegativePausedCondition = priorityMinimalThreshold < 0;
            this.PauseUpdateSelectors(
                updateNegativePausedCondition,
                commonPausedPredicate,
                this.updateNegativeItems,
                pausedSelectors);

            var updateZeroPausedCondition = priorityMinimalThreshold <= 0;
            Predicate<int> updateZeroPausedPredicate = priority => true;
            this.PauseUpdateSelectors(
                updateZeroPausedCondition,
                updateZeroPausedPredicate,
                this.updateZeroItems,
                pausedSelectors);

            var updatePositivePausedCondition = true;
            this.PauseUpdateSelectors(
                updatePositivePausedCondition,
                commonPausedPredicate,
                this.updatePositiveItems,
                pausedSelectors);
        }

        private void PauseUpdateSelectors(
            bool pausedCondition,
            Predicate<int> pausedPredicate,
            LinkedList<UpdateItem> pausedUpdateList,
            List<ICCUpdatable> pausedSelectors)
        {
            if (pausedCondition)
            {
                foreach (var entry in pausedUpdateList)
                {
                    if (pausedPredicate(entry.Priority))
                    {
                        entry.IsPaused = true;

                        if (!pausedSelectors.Contains(entry.Target))
                        {
                            pausedSelectors.Add(entry.Target);
                        }
                    }
                }
            }
        }

        #endregion

        #region Resume Operations

        public void Resume(List<ICCUpdatable> targets)
        {
            foreach (var target in targets)
            {
                this.Resume(target);
            }
        }

        public void Resume(ICCUpdatable target)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            CustomItemHash customSelector;
            if (this.TryGetCustomSelector(target, out customSelector))
            {
                customSelector.Paused = false;
            }

            UpdateItemHash updateSelector;
            if (this.TryGetUpdateSelector(target, out updateSelector))
            {
                updateSelector.Item.IsPaused = false;
            }
        }

        #endregion

        /// <summary>
        /// Starts the action manager.
        /// This would be called after UnscheduleAll() method has been called to restart the ActionManager.
        /// </summary>
        public void StartActionManager()
        {
            if (!this.IsActionManagerActive)
            {
                this.Schedule(this.ActionManager, CCSchedulePriority.System, false);
            }
        }

        #region Nested Types

        private class UpdateItem
        {
            /// <summary>
            /// Whether is marked for deletion.
            /// </summary>
            public bool IsDeleting;

            /// <summary>
            /// Whether is paused.
            /// </summary>
            public bool IsPaused;

            public int Priority;

            public ICCUpdatable Target;
        }

        private class UpdateItemHash
        {
            /// <summary>
            /// Item in the list.
            /// </summary>
            public UpdateItem Item;

            /// <summary>
            /// List it belongs to.
            /// </summary>
            public LinkedList<UpdateItem> List;

            /// <summary>
            /// Hash key.
            /// </summary>
            public ICCUpdatable Target;
        }

        private class CustomItemHash
        {
            public CCTimer CurrentTimer;

            public bool CurrentTimerSalvaged;

            public bool Paused;

            public ICCUpdatable Target;

            public int TimerIndex;

            public List<CCTimer> Timers;
        }

        #endregion
    }
}