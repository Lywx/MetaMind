namespace MetaMind.Engine.Nodes.Actions
{
    /// <summary>
    /// Actions are a set of transform you could apply to Node object. Action 
    /// class contains only data that characterizes the specific Action.
    /// </summary>
    public abstract class MMAction
    {
        #region Constructors and Finalizer

        protected MMAction()
        {
            this.Tag = (int)MMActionTag.Invalid;
        }

        #endregion Constructor

        #region Data

        /// <summary>
        /// Tag is used to group actions together.
        /// </summary>
        public int Tag { get; set; }

        #endregion

        #region Operations

        protected internal virtual MMActionState StartAction(IMMNode target)
        {
            return null;
        }

        #endregion
    }
}
