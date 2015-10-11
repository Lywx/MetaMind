namespace MetaMind.Engine.Entities.Nodes.Actions
{
    using System;

    /// <summary>
    /// ActionState is a base class for various ActionState pairing Action. It 
    /// contains the necessary information for running of the pairing Action.
    /// </summary>
    public abstract class MMActionState
    {
        #region Constructors

        /// <param name="action"></param>
        /// <param name="target">When target is null, the actions is stopped.</param>
        public MMActionState(MMAction action, IMMNode target)
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

            this.Target         = target;
            this.OriginalTarget = target;
        }

        #endregion

        #region Action Data

        public MMAction Action { get; protected set; }

        #endregion

        #region Target Data

        /// <summary>
        ///     Gets or sets the target.
        ///     Will be set with the 'StartAction' method of the corresponding Action.
        ///     When the 'Stop' method is called, Target will be set to null.
        /// </summary>
        /// <value>The target.</value>
        /// <remarks>
        ///     When target is null, the action is stopped.
        /// </remarks>
        public IMMNode Target { get; protected set; }

        public IMMNode OriginalTarget { get; protected set; }

        #endregion 

        #region State Data

        /// <summary>
        ///     Gets a value indicating whether this instance is done.
        /// </summary>
        /// <value><c>true</c> if this instance is done; otherwise, <c>false</c>.</value>
        public virtual bool IsDone => true;

        #endregion

        #region Operations

        /// <summary>
        ///     Called after the action has finished.
        ///     It will set the 'Target' to null.
        ///     IMPORTANT: You should never call this method manually. Instead, use: "target.StopAction(actionState);"
        /// </summary>
        protected internal virtual void Stop()
        {
            this.Target = null;
        }

        /// <summary>
        ///     Called every frame with it's delta time.
        ///     DON'T override unless you know what you are doing.
        /// </summary>
        /// <param name="dt">Delta Time</param>
        protected internal virtual void Step(float dt)
        {
        }

        /// <summary>
        ///     Called once per frame by this.Step()
        /// </summary>
        /// <param name="time">
        ///     A value between 0 and 1
        ///     For example:
        ///     0 means that the action just started
        ///     0.5 means that the action is in the middle
        ///     1 means that the action is over
        /// </param>
        public virtual void Update(float time)
        {
        }

        #endregion
    }
}