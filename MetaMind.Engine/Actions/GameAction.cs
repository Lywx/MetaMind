// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameAction.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>

namespace MetaMind.Engine.Actions
{
    using System;

    public class GameActionState
    {
        public MMActor Target { get; protected set; }

        public GameAction Action { get; protected set; }

        /// <summary>
        /// Gets a value indicating whether this instance is done.
        /// </summary>
        /// <value><c>true</c> if this instance is done; otherwise, <c>false</c>.</value>
        public virtual bool IsDone => true;

        public GameActionState(GameAction action, MMActor target)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            this.Action = action;
            this.Target = target;

            this.OriginalTarget = target;
            if (target != null)
                this.Layer = target.Layer;
        }

        /// <summary>
        /// Called after the action has finished.
        /// It will set the 'Target' to null. 
        /// IMPORTANT: You should never call this method manually. Instead, use: "target.StopAction(actionState);"
        /// </summary>
        protected internal virtual void Stop()
        {
            this.Target = null;
        }

        /// <summary>
        /// Called every frame with it's delta time. 
        /// 
        /// DON'T override unless you know what you are doing.
        /// 
        /// </summary>
        /// <param name="dt">Delta Time</param>
        protected internal virtual void Step(float dt)
        {
#if DEBUG
            CCLog.Log("[Action State step]. override me");
#endif
        }

        /// <summary>
        /// Called once per frame.
        /// </summary>
        /// <param name="time">A value between 0 and 1
        ///
        /// For example:
        ///
        /// 0 means that the action just started
        /// 0.5 means that the action is in the middle
        /// 1 means that the action is over</param>
        public virtual void Update(float time)
        {
#if DEBUG
            CCLog.Log("[Action State update]. override me");
#endif
        }

    }

    public class GameAction
    {
        protected internal virtual GameActionState StartAction(MMActor target)
        {
            return null;
        }
    }
}
