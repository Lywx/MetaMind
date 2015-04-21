namespace MetaMind.Engine
{
    using System;

    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class GameControllableEntity : GameVisualEntity, IInputable
    {
        #region States

        private bool controllable = true;

        public bool Controllable
        {
            get
            {
                return this.controllable;
            }

            set
            {
                if (this.controllable != value)
                {
                    this.controllable = value;
                    if (this.ControllableChanged != null)
                    {
                        this.ControllableChanged(this, EventArgs.Empty);
                    }

                    this.OnControllableChanged(this, EventArgs.Empty);
                }
            }
        }

        #endregion States

        #region Events

        public event EventHandler<EventArgs> ControllableChanged;

        public event EventHandler<EventArgs> InputOrderChanged;

        protected virtual void OnControllableChanged(object sender, EventArgs args)
        {
        }

        protected virtual void OnInputOrderChanged(object sender, EventArgs args)
        {
        }

        #region Engine Service

        protected IGameInputService Input { get; private set; }

        #endregion

        public GameControllableEntity()
        {
            this.Input = GameEngine.Service.Input;
        }

        #endregion Events

        #region Input

        private int inputOrder;

        public int InputOrder
        {
            get
            {
                return this.inputOrder;
            }

            set
            {
                if (this.inputOrder != value)
                {
                    this.inputOrder = value;
                    if (this.InputOrderChanged != null)
                    {
                        this.InputOrderChanged(this, null);
                    }

                    this.OnInputOrderChanged(this, null);
                }
            }
        }

        public virtual void UpdateInput(IGameInputService input, GameTime time) { }

        #endregion Input
    }
}