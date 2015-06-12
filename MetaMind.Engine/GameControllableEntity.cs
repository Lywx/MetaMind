namespace MetaMind.Engine
{
    using System;
    using System.Diagnostics;
    using System.Runtime.Serialization;

    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    [DataContract]
    public class GameControllableEntity : GameVisualEntity, IGameControllableEntity
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

        protected GameControllableEntity()
        {
            this.SetupService();
        }

        #region Events        

        public event EventHandler<EventArgs> ControllableChanged;

        public event EventHandler<EventArgs> InputOrderChanged;

        protected virtual void OnControllableChanged(object sender, EventArgs args)
        {
        }

        protected virtual void OnInputOrderChanged(object sender, EventArgs args)
        {
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

        #region Service

        protected IGameInputService GameInput { get; private set; }

        [OnDeserialized]
        private void SetupService(StreamingContext context)
        {
            this.SetupService();
        }

        private void SetupService()
        {
            if (GameEngine.Service != null)
            {
                this.GameInput = GameEngine.Service.Input;
            }
#if DEBUG
            else
            {
                Debug.WriteLine("Warning: GameEngine.Service = null");
            }
#endif
        }

        #endregion
    }
}