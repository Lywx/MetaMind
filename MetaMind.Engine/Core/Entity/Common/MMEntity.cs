namespace MetaMind.Engine.Core.Entity.Common
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Backend.Interop.Event;
    using Microsoft.Xna.Framework;

    [DataContract]
    public class MMEntity : MMObject, IMMEntity
    {
        #region Constructors and Finalizer 

        protected MMEntity()
        {
            this.EntityGuid = Guid.NewGuid();
        }

        ~MMEntity()
        {
            this.Dispose(true);
        }

        #endregion Destructors

        #region Events

        public event EventHandler<EventArgs> EntityEnabledChanged;

        public event EventHandler<EventArgs> EntityUpdateOrderChanged;

        #endregion

        #region Event Ons

        protected virtual void OnEntityEnabledChanged(object sender, EventArgs args)
        {
            this.EntityEnabledChanged?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnEntityUpdateOrderChanged(object sender, EventArgs args)
        {
            this.EntityUpdateOrderChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Entity Data

        public List<IMMEventListener> EntityListeners { get; set; } = new List<IMMEventListener>();

        public Guid EntityGuid { get; private set; }

        #endregion

        #region States

        private bool entityEnabled = true;

        public virtual bool EntityEnabled
        {
            get
            {
                return this.entityEnabled;
            }

            set
            {
                var changed = this.entityEnabled != value;
                if (changed)
                {
                    this.entityEnabled = value;

                    this.OnEntityEnabledChanged(this, null);
                }
            }
        }

        #endregion States

        #region Load and Unload

        public virtual void LoadContent()
        {
            this.EntityListeners.ForEach(l => this.GlobalInterop.Event.AddListener(l));
        }

        public virtual void UnloadContent()
        {
            this.EntityListeners.ForEach(l => this.GlobalInterop.Event.RemoveListener(l));
            this.EntityListeners.Clear();
        }

        #endregion Load and Unload

        #region Update

        private int entityUpdateOrder;

        public int EntityUpdateOrder
        {
            get
            {
                return this.entityUpdateOrder;
            }

            set
            {
                if (this.entityUpdateOrder != value)
                {
                    this.entityUpdateOrder = value;

                    this.OnEntityUpdateOrderChanged(this, null);
                }
            }
        }

        public virtual void Update(GameTime time) { }

        #endregion Update

        #region IDisposable

        private bool IsDisposed { get; set; }
     
        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!this.IsDisposed)
                {
                    // Dispose listeners
                    this.UnloadContent();

                    this.EntityUpdateOrderChanged = null;
                    this.EntityEnabledChanged     = null;

                    this.IsDisposed = true;
                }
            }
        }

        #endregion IDisposable
    }
}