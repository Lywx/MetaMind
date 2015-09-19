namespace MetaMind.Engine
{
    using System;
    using System.Runtime.Serialization;
    using Microsoft.Xna.Framework;
    using Service;

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
                if (this.controllable == value)
                {
                    return;
                }

                this.controllable = value;
                this.ControllableChanged?.Invoke(this, EventArgs.Empty);

                this.OnControllableChanged(this, EventArgs.Empty);
            }
        }

        #endregion States

        #region Constructors and Finalizer

        protected GameControllableEntity()
        {
        }

        ~GameControllableEntity()
        {
            this.Dispose(true);
        }

        #endregion

        #region Events        

        public event EventHandler<EventArgs> ControllableChanged = delegate { };

        public event EventHandler<EventArgs> InputOrderChanged = delegate { };

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
                    this.InputOrderChanged?.Invoke(this, null);

                    this.OnInputOrderChanged(this, null);
                }
            }
        }

        public virtual void UpdateInput(IGameInputService input, GameTime time) { }

        #endregion Input

        #region IDisposable

        private bool IsDisposed { get; set; }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    if (!this.IsDisposed)
                    {
                        this.ControllableChanged = null;
                        this.InputOrderChanged   = null;
                    }

                    this.IsDisposed = true;
                }
            }
            catch
            {
                // Ignored
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        #endregion
    }
}