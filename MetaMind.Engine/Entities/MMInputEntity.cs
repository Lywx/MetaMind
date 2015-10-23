namespace MetaMind.Engine.Entities
{
    using System;
    using System.Runtime.Serialization;
    using Microsoft.Xna.Framework;

    [DataContract]
    public class MMInputEntity : MMVisualEntity, IMMInputEntity
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

        protected MMInputEntity()
        {
        }

        ~MMInputEntity()
        {
            this.Dispose(true);
        }

        #endregion

        #region Comparison

        public int Compare(IMMInputable x, IMMInputable y)
        {
            return x.CompareTo(y);
        }

        public int CompareTo(IMMInputable other)
        {
            // Negate the result for the smaller the input order, the sooner the
            // input gets updated.
            return -this.InputOrder.CompareTo(other.InputOrder);
        }

        #endregion

        #region Events        

        public event EventHandler<EventArgs> InputableChanged = delegate { };

        public event EventHandler<EventArgs> InputOrderChanged = delegate { };

        #endregion Events

        #region Event Ons

        protected virtual void OnInputableChanged(object sender, EventArgs args)
        {
        }

        protected virtual void OnInputOrderChanged(object sender, EventArgs args)
        {
        }

        #endregion

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

        public virtual void UpdateInput(GameTime time) { }

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
                        this.DisposeEvents();
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

        private void DisposeEvents()
        {
            this.InputableChanged = null;
            this.InputOrderChanged = null;
        }

        #endregion
    }
}