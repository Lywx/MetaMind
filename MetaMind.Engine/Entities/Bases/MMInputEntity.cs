namespace MetaMind.Engine.Entities.Bases
{
    using System;
    using System.Runtime.Serialization;
    using Microsoft.Xna.Framework;

    [DataContract]
    public class MMInputEntity : MMVisualEntity, IMMInputEntity
    {
        #region States

        private bool entityInputable = true;

        public bool EntityInputable
        {
            get
            {
                return this.entityInputable;
            }

            set
            {
                if (this.entityInputable == value)
                {
                    return;
                }

                this.entityInputable = value;
                this.EntityInputableChanged?.Invoke(this, EventArgs.Empty);

                this.OnEntityInputableChanged(this, EventArgs.Empty);
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
            return -this.EntityInputOrder.CompareTo(other.EntityInputOrder);
        }

        #endregion

        #region Events        

        public event EventHandler<EventArgs> EntityInputableChanged = delegate { };

        public event EventHandler<EventArgs> EntityInputOrderChanged = delegate { };

        #endregion Events

        #region Event Ons

        protected virtual void OnEntityInputableChanged(object sender, EventArgs args)
        {
        }

        protected virtual void OnEntityInputOrderChanged(object sender, EventArgs args)
        {
        }

        #endregion

        #region Input

        private int entityInputOrder;

        public int EntityInputOrder
        {
            get
            {
                return this.entityInputOrder;
            }

            set
            {
                if (this.entityInputOrder != value)
                {
                    this.entityInputOrder = value;
                    this.EntityInputOrderChanged?.Invoke(this, null);

                    this.OnEntityInputOrderChanged(this, null);
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
            this.EntityInputableChanged = null;
            this.EntityInputOrderChanged = null;
        }

        #endregion
    }
}