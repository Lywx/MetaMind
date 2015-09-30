namespace MetaMind.Engine.Gui.Reactors
{
    using System;

    public abstract class MMReactor : MMInputableEntity, IGameReactor
    {
        #region State

        public bool Active { get; set; } = true;

        #endregion

        #region Initialization

        public bool Initialized { get; private set; }

        public virtual void Initialize()
        {
            this.LoadContent(this.Interop);

            this.Initialized = true;
        }

        public void CheckInitialization()
        {
            if (!this.Initialized)
            {
                throw new InvalidOperationException("Settings are not initialized.");
            }
        }

        #endregion
    }
}