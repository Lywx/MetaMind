namespace MetaMind.Engine.Nodes
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using NLog;
    using Actions;

    public static class CCSchedulePriority
    {
        /// We will define this as a static class since we can not define and enum with the way uint.MaxValue is represented.
        public const uint RepeatForever = uint.MaxValue - 1;

        public const int System = int.MinValue;

        public const int User = System + 1;
    }

    /// <summary>
    /// Scheduler is responsible for triggering the scheduled callbacks.
    /// You should not use NSTimer. Instead use this class.
    ///
    /// There are 2 different types of callbacks (selectors):
    /// 
    /// - update selector: the 'update' selector will be called every frame. You can customize the priority.
    /// - custom selector: A custom selector will be called every frame, or with a custom interval of time
    ///
    /// The 'custom selectors' should be avoided when possible. It is faster, and consumes less memory to use the 'update selector'.
    /// </summary>
    public class CCScheduler
    {
        #region Logger

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

        private static HashTimeEntry[] tmpHashSelectorArray = new HashTimeEntry[128];

        private static ICCUpdatable[] tmpSelectorArray = new ICCUpdatable[128];

        private readonly Dictionary<ICCUpdatable, HashTimeEntry> hashForTimers = new Dictionary<ICCUpdatable, HashTimeEntry>();

        private readonly Dictionary<ICCUpdatable, HashUpdateEntry> hashForUpdates = new Dictionary<ICCUpdatable, HashUpdateEntry>();

        /// <summary>
        /// List of entity for priority == 0 
        /// </summary>
        /// <remarks>
        /// Hash used to fetch quickly the list entries for pause, delete, etc.
        /// </remarks>
        private readonly LinkedList<ListEntry> updates0List = new LinkedList<ListEntry>(); 

        /// <summary>
        /// List of entity for priority < 0 
        /// </summary>
        private readonly LinkedList<ListEntry> updatesNegList = new LinkedList<ListEntry>(); 

        /// <summary>
        /// List of entity for priority > 0 
        /// </summary>
        private readonly LinkedList<ListEntry> updatesPosList = new LinkedList<ListEntry>(); 

        private HashTimeEntry currentTarget;

        private bool isCurrentTargetSalvaged;

        private bool isUpdateHashLocked;

        public float TimeScale { get; set; }

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

                    LinkedListNode<ListEntry> next;

                    for (var node = this.updatesNegList.First; node != null; node = next)
                    {
                        next = node.Next;

                        if (node.Value.Target == target && 
                           !node.Value.MarkedForDeletion)
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

        internal CCScheduler(MMActionManager actionManager = null)
        {
            this.ActionManager = actionManager;

            this.TimeScale = 1.0f;
        }

        #endregion Constructors

        #region Schedule Operations

        /// <remarks>
        /// The scheduled method will be called every 'interval' seconds.
        /// If paused is YES, then it won't be called until it is resumed.
        /// If 'interval' is 0, it will be called every frame, but if so, it's recommended to use 'scheduleUpdateForTarget:' instead.
        /// If the selector is already scheduled, then only the interval parameter will be updated without re-scheduling it again.
        /// repeat let the action be repeated repeat + 1 times, use RepeatForever to let the action run continuously
        /// delay is the amount of time the action will wait before it'll start
        /// @since v0.99.3, repeat and delay added in v1.1
        /// </remarks>>
        public void Schedule(Action<float> selector, ICCUpdatable target, float interval, uint repeat,
                                     float delay, bool paused)
        {
            Debug.Assert(selector != null);
            Debug.Assert(target != null);

            HashTimeEntry element;

            lock (this.hashForTimers)
            {
                if (!this.hashForTimers.TryGetValue(target, out element))
                {
                    element = new HashTimeEntry { Target = target };
                    this.hashForTimers[target] = element;

                    // Is this the 1st element ? Then set the pause level to all the selectors of this target
                    element.Paused = paused;
                }
                else
                {
                    if (element != null)
                    {
                        Debug.Assert(element.Paused == paused, "CCScheduler.Schedule: All are paused");
                    }
                }

                if (element != null)
                {
                    if (element.Timers == null)
                    {
                        element.Timers = new List<CCTimer>();
                    }
                    else
                    {
                        var timers = element.Timers.ToArray();
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

                    element.Timers.Add(new CCTimer(this, target, selector, interval, repeat, delay));
                }
            }
        }

        /** Schedules the 'update' selector for a given target with a given priority.
    	     The 'update' selector will be called every frame.
    	     The lower the priority, the earlier it is called.
    	     @since v0.99.3
    	     */

        public void Schedule(ICCUpdatable targt, int priority, bool paused)
        {
            HashUpdateEntry element;

            if (this.hashForUpdates.TryGetValue(targt, out element))
            {
                Debug.Assert(element.Entry.MarkedForDeletion);

                // TODO: check if priority has changed!
                element.Entry.MarkedForDeletion = false;

                return;
            }

            // most of the updates are going to be 0, that's way there
            // is an special list for updates with priority 0
            if (priority == 0)
            {
                this.AppendIn(this.updates0List, targt, paused);
            }
            else if (priority < 0)
            {
                this.PriorityIn(this.updatesNegList, targt, priority, paused);
            }
            else
            {
                this.PriorityIn(this.updatesPosList, targt, priority, paused);
            }
        }

        /** Unschedule a selector for a given target.
    	     If you want to unschedule the "update", use unscheudleUpdateForTarget.
    	     @since v0.99.3
    	     */

        public void Unschedule(Action<float> selector, ICCUpdatable target)
        {
            // explicitly handle nil arguments when removing an object
            if (selector == null || target == null)
            {
                return;
            }

            HashTimeEntry element;

            if (this.hashForTimers.TryGetValue(target, out element))
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

                        // update timerIndex in case we are in tick:, looping over the actions
                        if (element.TimerIndex >= i)
                        {
                            element.TimerIndex--;
                        }

                        if (element.Timers.Count == 0)
                        {
                            if (this.currentTarget == element)
                            {
                                this.isCurrentTargetSalvaged = true;
                            }
                            else
                            {
                                this.RemoveHashElement(element);
                            }
                        }

                        return;
                    }
                }
            }
        }

        /// <remarks>
        /// Unschedules all selectors for a given target.
    	/// This also includes the "update" selector.
    	/// @since v0.99.3
        /// </remarks>
        public void Unschedule(ICCUpdatable target)
        {
            if (target == null)
            {
                return;
            }

            HashUpdateEntry element;

            if (this.hashForUpdates.TryGetValue(target, out element))
            {
                if (this.isUpdateHashLocked)
                {
                    element.Entry.MarkedForDeletion = true;
                }
                else
                {
                    this.RemoveUpdateFromHash(element.Entry);
                }
            }
        }

        public void UnscheduleAll(int minPriority)
        {
            var count = this.hashForTimers.Values.Count;
            if (tmpHashSelectorArray.Length < count)
            {
                tmpHashSelectorArray = new HashTimeEntry[tmpHashSelectorArray.Length * 2];
            }

            this.hashForTimers.Values.CopyTo(tmpHashSelectorArray, 0);

            for (int i = 0; i < count; i++)
            {
                // Element may be removed in unscheduleAllSelectorsForTarget
                this.UnscheduleAll(tmpHashSelectorArray[i].Target);
            }

            // Updates selectors
            if (minPriority < 0 && this.updatesNegList.Count > 0)
            {
                LinkedList<ListEntry> copy = new LinkedList<ListEntry>(this.updatesNegList);
                foreach (ListEntry entry in copy)
                {
                    if (entry.Priority >= minPriority)
                    {
                        this.UnscheduleAll(entry.Target);
                    }
                }
            }

            if (minPriority <= 0 && this.updates0List.Count > 0)
            {
                LinkedList<ListEntry> copy = new LinkedList<ListEntry>(this.updates0List);
                foreach (ListEntry entry in copy)
                {
                    this.UnscheduleAll(entry.Target);
                }
            }

            if (this.updatesPosList.Count > 0)
            {
                LinkedList<ListEntry> copy = new LinkedList<ListEntry>(this.updatesPosList);
                foreach (ListEntry entry in copy)
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
            // explicit NULL handling
            if (target == null)
            {
                return;
            }

            // custom selectors           
            HashTimeEntry element;

            if (this.hashForTimers.TryGetValue(target, out element))
            {
                if (element.Timers.Contains(element.CurrentTimer))
                {
                    element.CurrentTimerSalvaged = true;
                }
                element.Timers.Clear();

                if (this.currentTarget == element)
                {
                    this.isCurrentTargetSalvaged = true;
                }
                else
                {
                    this.RemoveHashElement(element);
                }
            }

            // update selector
            this.Unschedule(target);
        }

        public void UnscheduleAll()
        {
            // This also stops ActionManger from updating which means all actions are stopped as well.
            this.UnscheduleAll(CCSchedulePriority.System);
        }

        #endregion

        #region Update

        internal void Update(float dt)
        {
            this.isUpdateHashLocked = true;

            try
            {
                if (this.TimeScale != 1.0f)
                {
                    dt *= this.TimeScale;
                }

                LinkedListNode<ListEntry> next;

                // updates with priority < 0
                //foreach (ListEntry entry in _updatesNegList)
                for (LinkedListNode<ListEntry> node = this.updatesNegList.First; node != null; node = next)
                {
                    next = node.Next;
                    if (!node.Value.Paused && !node.Value.MarkedForDeletion)
                    {
                        node.Value.Target.Update(dt);
                    }
                }

                // updates with priority == 0
                //foreach (ListEntry entry in _updates0List)
                for (LinkedListNode<ListEntry> node = this.updates0List.First; node != null; node = next)
                {
                    next = node.Next;
                    if (!node.Value.Paused && !node.Value.MarkedForDeletion)
                    {
                        node.Value.Target.Update(dt);
                    }
                }

                // updates with priority > 0
                for (LinkedListNode<ListEntry> node = this.updatesPosList.First; node != null; node = next)
                {
                    next = node.Next;
                    if (!node.Value.Paused && !node.Value.MarkedForDeletion)
                    {
                        node.Value.Target.Update(dt);
                    }
                }

                // Iterate over all the custom selectors
                var count = this.hashForTimers.Count;
                if (count > 0)
                {
                    if (tmpSelectorArray.Length < count)
                    {
                        tmpSelectorArray = new ICCUpdatable[tmpSelectorArray.Length * 2];
                    }
                    this.hashForTimers.Keys.CopyTo(tmpSelectorArray, 0);

                    for (int i = 0; i < count; i++)
                    {
                        ICCUpdatable key = tmpSelectorArray[i];
                        if (!this.hashForTimers.ContainsKey(key))
                        {
                            continue;
                        }
                        HashTimeEntry elt = this.hashForTimers[key];

                        this.currentTarget = elt;
                        this.isCurrentTargetSalvaged = false;

                        if (!this.currentTarget.Paused)
                        {
                            // The 'timers' array may change while inside this loop
                            for (elt.TimerIndex = 0; elt.TimerIndex < elt.Timers.Count; ++elt.TimerIndex)
                            {
                                elt.CurrentTimer = elt.Timers[elt.TimerIndex];
                                if (elt.CurrentTimer != null)
                                {
                                    elt.CurrentTimerSalvaged = false;

                                    elt.CurrentTimer.Update(dt);

                                    elt.CurrentTimer = null;
                                }
                            }
                        }

                        // only delete currentTarget if no actions were scheduled during the cycle (issue #481)
                        if (this.isCurrentTargetSalvaged && this.currentTarget.Timers.Count == 0)
                        {
                            this.RemoveHashElement(this.currentTarget);
                        }
                    }
                }

                // delete all updates that are marked for deletion
                // updates with priority < 0
                for (LinkedListNode<ListEntry> node = this.updatesNegList.First; node != null; node = next)
                {
                    next = node.Next;
                    if (node.Value.MarkedForDeletion)
                    {
                        this.updatesNegList.Remove(node);
                        this.RemoveUpdateFromHash(node.Value);
                    }
                }

                // updates with priority == 0
                for (LinkedListNode<ListEntry> node = this.updates0List.First; node != null; node = next)
                {
                    next = node.Next;
                    if (node.Value.MarkedForDeletion)
                    {
                        this.updates0List.Remove(node);
                        this.RemoveUpdateFromHash(node.Value);
                    }
                }

                // updates with priority > 0
                for (LinkedListNode<ListEntry> node = this.updatesPosList.First; node != null; node = next)
                {
                    next = node.Next;
                    if (node.Value.MarkedForDeletion)
                    {
                        this.updatesPosList.Remove(node);
                        this.RemoveUpdateFromHash(node.Value);
                    }
                }
            }
            finally
            {
                // Always do this just in case there is a problem

                this.isUpdateHashLocked = false;
                this.currentTarget = null;
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

        public List<ICCUpdatable> PauseAllTargets()
        {
            return this.PauseAllTargets(int.MinValue);
        }

        public List<ICCUpdatable> PauseAllTargets(int minPriority)
        {
            var idsWithSelectors = new List<ICCUpdatable>();

            // Custom Selectors
            foreach (HashTimeEntry element in this.hashForTimers.Values)
            {
                element.Paused = true;
                if (!idsWithSelectors.Contains(element.Target))
                    idsWithSelectors.Add(element.Target);
            }

            // Updates selectors
            if (minPriority < 0)
            {
                foreach (ListEntry element in this.updatesNegList)
                {
                    if (element.Priority >= minPriority)
                    {
                        element.Paused = true;
                        if (!idsWithSelectors.Contains(element.Target))
                            idsWithSelectors.Add(element.Target);
                    }
                }
            }

            if (minPriority <= 0)
            {
                foreach (ListEntry element in this.updates0List)
                {
                    element.Paused = true;
                    if (!idsWithSelectors.Contains(element.Target))
                        idsWithSelectors.Add(element.Target);
                }
            }

            if (minPriority < 0)
            {
                foreach (ListEntry element in this.updatesPosList)
                {
                    if (element.Priority >= minPriority)
                    {
                        element.Paused = true;
                        if (!idsWithSelectors.Contains(element.Target))
                            idsWithSelectors.Add(element.Target);
                    }
                }
            }

            return idsWithSelectors;
        }

        public void PauseTarget(ICCUpdatable target)
        {
            Debug.Assert(target != null);

            // custom selectors
            HashTimeEntry entry;
            if (this.hashForTimers.TryGetValue(target, out entry))
            {
                entry.Paused = true;
            }

            // Update selector
            HashUpdateEntry updateEntry;
            if (this.hashForUpdates.TryGetValue(target, out updateEntry))
            {
                updateEntry.Entry.Paused = true;
            }
        }

        public void Resume(List<ICCUpdatable> targetsToResume)
        {
            foreach (ICCUpdatable target in targetsToResume)
            {
                this.Resume(target);
            }
        }

        public void Resume(ICCUpdatable target)
        {
            Debug.Assert(target != null);

            // custom selectors
            HashTimeEntry element;
            if (this.hashForTimers.TryGetValue(target, out element))
            {
                element.Paused = false;
            }

            // Update selector
            HashUpdateEntry elementUpdate;
            if (this.hashForUpdates.TryGetValue(target, out elementUpdate))
            {
                elementUpdate.Entry.Paused = false;
            }
        }

        public bool IsTargetPaused(ICCUpdatable target)
        {
            Debug.Assert(target != null, "target must be non nil");

            // Custom selectors
            HashTimeEntry element;
            if (this.hashForTimers.TryGetValue(target, out element))
            {
                return element.Paused;
            }

            // We should check update selectors if target does not have custom selectors
            HashUpdateEntry elementUpdate;
            if (this.hashForUpdates.TryGetValue(target, out elementUpdate))
            {
                return elementUpdate.Entry.Paused;
            }

            return false; // should never get here
        }

        void RemoveHashElement(HashTimeEntry element)
        {
            this.hashForTimers.Remove(element.Target);

            element.Timers.Clear();
            element.Target = null;
        }

        private void RemoveUpdateFromHash(ListEntry entry)
        {
            HashUpdateEntry element;

            if (this.hashForUpdates.TryGetValue(entry.Target, out element))
            {
                // Remove from list entry
                element.List.Remove(entry);
                element.Entry = null;

                // Remove from hash entry
                this.hashForUpdates.Remove(entry.Target);
                element.Target = null;
            }
        }

        private void PriorityIn(
            LinkedList<ListEntry> list,
            ICCUpdatable target,
            int priority,
            bool paused)
        {
            var listElement = new ListEntry
            {
                Target = target,
                Priority = priority,
                Paused = paused,
                MarkedForDeletion = false
            };

            if (list.First == null)
            {
                list.AddFirst(listElement);
            }
            else
            {
                var added = false;
                for (var node = list.First; node != null; node = node.Next)
                {
                    if (priority < node.Value.Priority)
                    {
                        list.AddBefore(node, listElement);
                        added = true;
                        break;
                    }
                }

                if (!added)
                {
                    list.AddLast(listElement);
                }
            }

            // update hash entry for quick access
            var hashElement = new HashUpdateEntry
            {
                Target = target,
                List = list,
                Entry = listElement
            };

            this.hashForUpdates.Add(target, hashElement);
        }

        private void AppendIn(
            LinkedList<ListEntry> list,
            ICCUpdatable target,
            bool paused)
        {
            var entry = new ListEntry
            {
                Target = target,
                Paused = paused,
                MarkedForDeletion = false
            };

            list.AddLast(entry);

            // update hash entry for quicker access
            var hashElement = new HashUpdateEntry
            {
                Target = target,
                List   = list,
                Entry  = entry
            };

            this.hashForUpdates.Add(target, hashElement);
        }

        #region Nested Type: HashSelectorEntry

        private class HashTimeEntry
        {
            public CCTimer CurrentTimer;

            public bool CurrentTimerSalvaged;

            public bool Paused;

            public ICCUpdatable Target;

            public int TimerIndex;

            public List<CCTimer> Timers;
        }

        #endregion

        #region Nested Type: HashUpdateEntry

        private class HashUpdateEntry
        {
            /// <summary>
            /// Entry in the list.
            /// </summary>
            public ListEntry Entry;

            /// <summary>
            // List it belongs to.
            /// </summary>
            public LinkedList<ListEntry> List; 

            /// <summary>
            ///     Hash key.
            /// </summary>
            public ICCUpdatable Target; 
        }

        #endregion

        #region Nested Type: ListEntry

        private class ListEntry
        {
            /// <summary>
            /// Flag for deletion.
            /// </summary>
            public bool MarkedForDeletion;

            public bool Paused;

            public int Priority;

            public ICCUpdatable Target;
        }

        #endregion
    }
}
