namespace MetaMind.Engine.Entities
{
    using System;
    using System.Runtime.Serialization;
    using Microsoft.Xna.Framework;
    using Service;

    [DataContract]
    public class MMInputableEntity : MMVisualEntity, IMMInputableEntity
    {
        #region States

        private bool inputable = true;

        public bool Inputable
        {
            get
            {
                return this.inputable;
            }

            set
            {
                if (this.inputable == value)
                {
                    return;
                }

                this.inputable = value;
                this.InputableChanged?.Invoke(this, EventArgs.Empty);

                this.OnInputableChanged(this, EventArgs.Empty);
            }
        }

        #endregion States

        #region Constructors and Finalizer

        protected MMInputableEntity()
        {
        }

        ~MMInputableEntity()
        {
            this.Dispose(true);
        }

        #endregion

        #region Events        

        public event EventHandler<EventArgs> InputableChanged = delegate { };

        public event EventHandler<EventArgs> InputOrderChanged = delegate { };

        protected virtual void OnInputableChanged(object sender, EventArgs args)
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

        public virtual void UpdateInput(IMMEngineInputService input, GameTime time) { }

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
                        this.InputableChanged = null;
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