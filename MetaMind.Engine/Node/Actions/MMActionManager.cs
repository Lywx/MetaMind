// --------------------------------------------------------------------------------------------------------------------
// <copyright file="">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>

namespace MetaMind.Engine.Node.Actions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// ActionManager is 
    /// </summary>
    public class MMActionManager : ICCUpdatable, IDisposable
    {
        internal class HashElement
        {
            public int ActionIndex;

            public List<MMActionState> ActionStates;

            public MMActionState CurrentActionState;

            public bool CurrentActionSalvaged;

            public bool Paused;

            public object Target;
        }

        private static MMNode[] tmpKeysArray = new MMNode[128];

        private readonly Dictionary<object, HashElement> targets = new Dictionary<object, HashElement>();

        private bool currentTargetSalvaged;

        private HashElement currentTarget;

        private bool targetsAvailable;

        #region Constructors and Finalizer

        public MMActionManager()
        {
            
        }

        ~MMActionManager()
        {
            this.Dispose(false);
        }

        #endregion

        #region IDisposal

        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Dispose of managed resources
            }

            this.RemoveAllActions();
        }

        #endregion

        public MMAction GetAction(int tag, MMNode target)
        {
            Debug.Assert(tag != (int)MMActionTag.Invalid);

            // Early out if we do not have any targets to search
            if (this.targets.Count == 0)
                return null;

            HashElement element;
            if (this.targets.TryGetValue(target, out element))
            {
                if (element.ActionStates != null)
                {
                    int limit = element.ActionStates.Count;
                    for (int i = 0; i < limit; i++)
                    {
                        var action = element.ActionStates[i].Action;

                        if (action.Tag == tag)
                        {
                            return action;
                        }
                    }

                    MMLog.Log("CocosSharp : GetActionByTag: Tag " + tag + " not found");
                }
            }
            else
            {
                MMLog.Log("CocosSharp : GetActionByTag: Target not found");
            }
            return null;
        }

        public MMActionState GetActionState(int tag, MMNode target)
        {
            Debug.Assert(tag != (int)MMActionTag.Invalid);

            // Early out if we do not have any targets to search
            if (this.targets.Count == 0)
                return null;

            HashElement element;
            if (this.targets.TryGetValue(target, out element))
            {
                if (element.ActionStates != null)
                {
                    int limit = element.ActionStates.Count;
                    for (int i = 0; i < limit; i++)
                    {
                        var actionState = element.ActionStates[i];

                        if (actionState.Action.Tag == tag)
                        {
                            return actionState;
                        }
                    }
                    MMLog.Log("CocosSharp : GetActionStateByTag: Tag " + tag + " not found");
                }
            }
            else
            {
                MMLog.Log("CocosSharp : GetActionStateByTag: Target not found");
            }
            return null;
        }

        public int NumberOfRunningActionsInTarget(MMNode target)
        {
            HashElement element;
            if (this.targets.TryGetValue(target, out element))
            {
                return (element.ActionStates != null) ? element.ActionStates.Count : 0;
            }
            return 0;
        }

        public void Update(float dt)
        {
            if (!this.targetsAvailable)
                return;

            int count = this.targets.Count;

            while (tmpKeysArray.Length < count)
            {
                tmpKeysArray = new MMNode[tmpKeysArray.Length * 2];
            }

            this.targets.Keys.CopyTo(tmpKeysArray, 0);

            for (int i = 0; i < count; i++)
            {
                HashElement elt;
                if (!this.targets.TryGetValue(tmpKeysArray[i], out elt))
                {
                    continue;
                }

                this.currentTarget = elt;
                this.currentTargetSalvaged = false;

                if (!this.currentTarget.Paused)
                {
                    // The 'actions' may change while inside this loop.
                    for (this.currentTarget.ActionIndex = 0;
                        this.currentTarget.ActionIndex < this.currentTarget.ActionStates.Count;
                        this.currentTarget.ActionIndex++)
                    {

                        this.currentTarget.CurrentActionState = this.currentTarget.ActionStates[this.currentTarget.ActionIndex];
                        if (this.currentTarget.CurrentActionState == null)
                        {
                            continue;
                        }

                        this.currentTarget.CurrentActionSalvaged = false;

                        this.currentTarget.CurrentActionState.Step(dt);

                        if (this.currentTarget.CurrentActionSalvaged)
                        {
                            // The currentAction told the node to remove it. To prevent the action from
                            // accidentally deallocating itself before finishing its step, we retained
                            // it. Now that step is done, it's safe to release it.

                            //currentTarget->currentAction->release();
                        }
                        else if (this.currentTarget.CurrentActionState.IsDone)
                        {
                            this.currentTarget.CurrentActionState.Stop();

                            var actionState = this.currentTarget.CurrentActionState;
                            // Make currentAction nil to prevent removeAction from salvaging it.
                            this.currentTarget.CurrentActionState = null;
                            this.RemoveAction(actionState);
                        }
                        this.currentTarget.CurrentActionState = null;
                    }
                }

                // only delete currentTarget if no actions were scheduled during the cycle (issue #481)
                if (this.currentTargetSalvaged && this.currentTarget.ActionStates.Count == 0)
                {
                    this.DeleteHashElement(this.currentTarget);
                }
            }

            // issue #635
            this.currentTarget = null;
        }

        internal void DeleteHashElement(HashElement element)
        {
            element.ActionStates.Clear();
            this.targets.Remove(element.Target);
            element.Target = null;
            this.targetsAvailable = this.targets.Count > 0;
        }

        internal void ActionAllocWithHashElement(HashElement element)
        {
            if (element.ActionStates == null)
            {
                element.ActionStates = new List<MMActionState>();
            }
        }


        #region Action running

        public void PauseTarget(object target)
        {
            HashElement element;
            if (this.targets.TryGetValue(target, out element))
            {
                element.Paused = true;
            }
        }

        public void ResumeTarget(object target)
        {
            HashElement element;
            if (this.targets.TryGetValue(target, out element))
            {
                element.Paused = false;
            }
        }

        public List<object> PauseAllRunningActions()
        {
            var idsWithActions = new List<object>();

            foreach (var element in this.targets.Values)
            {
                if (!element.Paused)
                {
                    element.Paused = true;
                    idsWithActions.Add(element.Target);
                }
            }

            return idsWithActions;
        }

        public void ResumeTargets(List<object> targetsToResume)
        {
            for (int i = 0; i < targetsToResume.Count; i++)
            {
                this.ResumeTarget(targetsToResume[i]);
            }
        }

        #endregion Action running


        #region Adding/removing actions

        public MMActionState AddAction(MMAction action, MMNode target, bool paused = false)
        {
            Debug.Assert(action != null);
            Debug.Assert(target != null);

            HashElement element;
            if (!this.targets.TryGetValue(target, out element))
            {
                element = new HashElement();
                element.Paused = paused;
                element.Target = target;
                this.targets.Add(target, element);
                this.targetsAvailable = true;
            }

            this.ActionAllocWithHashElement(element);
            var isActionRunning = false;
            foreach (var existingState in element.ActionStates)
            {
                if (existingState.Action == action)
                {
                    isActionRunning = true;
                    break;
                }
            }
            Debug.Assert(!isActionRunning, "CocosSharp : Action is already running for this target.");
            var state = action.StartAction(target);
            element.ActionStates.Add(state);

            return state;
        }

        public void RemoveAllActions()
        {
            if (!this.targetsAvailable)
                return;

            int count = this.targets.Count;
            if (tmpKeysArray.Length < count)
            {
                tmpKeysArray = new MMNode[tmpKeysArray.Length * 2];
            }

            this.targets.Keys.CopyTo(tmpKeysArray, 0);

            for (int i = 0; i < count; i++)
            {
                this.RemoveAllActionsFromTarget(tmpKeysArray[i]);
            }
        }

        public void RemoveAllActionsFromTarget(MMNode target)
        {
            if (target == null)
            {
                return;
            }

            HashElement element;
            if (this.targets.TryGetValue(target, out element))
            {
                if (element.ActionStates.Contains(element.CurrentActionState) && (!element.CurrentActionSalvaged))
                {
                    element.CurrentActionSalvaged = true;
                }

                element.ActionStates.Clear();

                if (this.currentTarget == element)
                {
                    this.currentTargetSalvaged = true;
                }
                else
                {
                    this.DeleteHashElement(element);
                }
            }
        }

        public void RemoveAction(MMActionState actionState)
        {
            if (actionState == null || actionState.OriginalTarget == null)
            {
                return;
            }

            object target = actionState.OriginalTarget;
            HashElement element;
            if (this.targets.TryGetValue(target, out element))
            {
                int i = element.ActionStates.IndexOf(actionState);

                if (i != -1)
                {
                    this.RemoveActionAtIndex(i, element);
                }
                else
                {
                    MMLog.Log("CocosSharp: removeAction: Action not found");
                }
            }
            else
            {
                MMLog.Log("CocosSharp: removeAction: Target not found");
            }
        }

        internal void RemoveActionAtIndex(int index, HashElement element)
        {
            var action = element.ActionStates[index];

            if (action == element.CurrentActionState && (!element.CurrentActionSalvaged))
            {
                element.CurrentActionSalvaged = true;
            }

            element.ActionStates.RemoveAt(index);

            // update actionIndex in case we are in tick. looping over the actions
            if (element.ActionIndex >= index)
            {
                element.ActionIndex--;
            }

            if (element.ActionStates.Count == 0)
            {
                if (this.currentTarget == element)
                {
                    this.currentTargetSalvaged = true;
                }
                else
                {
                    this.DeleteHashElement(element);
                }
            }
        }

        internal void RemoveAction(MMAction action, MMNode target)
        {
            if (action == null || target == null)
                return;

            HashElement element;
            if (this.targets.TryGetValue(target, out element))
            {
                int limit = element.ActionStates.Count;
                bool actionFound = false;

                for (int i = 0; i < limit; i++)
                {
                    var actionState = element.ActionStates[i];

                    if (actionState.Action == action && actionState.OriginalTarget == target)
                    {
                        this.RemoveActionAtIndex(i, element);
                        actionFound = true;
                        break;
                    }
                }

                if (!actionFound)
                    MMLog.Log("CocosSharp : RemoveAction: Action not found");
            }
            else
            {
                MMLog.Log("CocosSharp : RemoveAction: Target not found");
            }

        }

        public void RemoveAction(int tag, MMNode target)
        {
            Debug.Assert((tag != (int)MMActionTag.Invalid));
            Debug.Assert(target != null);

            // Early out if we do not have any targets to search
            if (this.targets.Count == 0)
                return;

            HashElement element;
            if (this.targets.TryGetValue(target, out element))
            {
                int limit = element.ActionStates.Count;
                bool tagFound = false;

                for (int i = 0; i < limit; i++)
                {
                    var actionState = element.ActionStates[i];

                    if (actionState.Action.Tag == tag && actionState.OriginalTarget == target)
                    {
                        this.RemoveActionAtIndex(i, element);
                        tagFound = true;
                        break;
                    }
                }

                if (!tagFound)
                    MMLog.Log("CocosSharp : removeActionByTag: Tag " + tag + " not found");
            }
            else
            {
                MMLog.Log("CocosSharp : removeActionByTag: Target not found");
            }
        }

        #endregion Adding/removing actions
    }

}
