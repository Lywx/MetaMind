namespace MetaMind.Engine.Gui.Components
{
    using System;

    public abstract class Component : GameInputableEntity, IComponent
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