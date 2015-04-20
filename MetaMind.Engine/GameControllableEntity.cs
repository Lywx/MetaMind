namespace MetaMind.Engine
{
    using System;

    using MetaMind.Engine.Components.Inputs;

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

        private static bool isFlyweightServiceLoaded;

        protected static IGameInput GameInput { get; private set; }

        #endregion

        public GameControllableEntity()
            : this(GameEngine.Service.GameInput)
        {
        }

        public GameControllableEntity(IGameInput gameInput)
        {
            if (!isFlyweightServiceLoaded)
            {
                GameInput = gameInput;

                isFlyweightServiceLoaded = true;
            }
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

        public virtual void UpdateInput(IGameInput gameInput, GameTime gameTime) { }

        #endregion Input
    }
}