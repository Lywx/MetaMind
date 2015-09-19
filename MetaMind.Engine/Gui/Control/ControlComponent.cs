namespace MetaMind.Engine.Gui.Control
{
    using System;

    public abstract class ControlComponent : GameControllableEntity, IComponent
    {
        public ControlComponent(ControlManager manager)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager), @"Component cannot be created. Manager instance is needed.");
            }

            this.Manager = manager;
        }

        public ControlComponent()
        {
        }

        #region Manager

        public ControlManager Manager { get; set; } = null;

        public bool Managed => this.Manager != null;

        #endregion

        #region Initialization

        public bool Initialized { get; private set; }

        public virtual void Initialize()
        {
            this.Initialized = true;
        }

        #endregion
    }
}