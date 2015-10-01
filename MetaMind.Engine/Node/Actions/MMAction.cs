namespace MetaMind.Engine.Node.Actions
{
    /// <summary>
    /// Actions are a set of transform you could apply to Node object. Action 
    /// class contains only data that characterizes the specific Action.
    /// </summary>
    public abstract class MMAction
    {
        #region Constructors

        protected MMAction()
        {
            this.Tag = (int)MMActionTag.Invalid;
        }

        #endregion Constructor

        #region Data

        public int Tag { get; set; }

        #endregion

        #region Operations

        protected internal virtual MMActionState StartAction(MMNode target)
        {
            return null;
        }

        #endregion
    }
}
