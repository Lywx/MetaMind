namespace MetaMind.Engine.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using Components.Interop.Event;
    using Microsoft.Xna.Framework;
    using Services;

    [DataContract]
    public class MMEntity : MMObject, IMMEntity
    {
        #region Constructors and Finalizer 

        protected MMEntity()
        {
            this.Guid      = Guid.NewGuid();
            this.Listeners = new List<IMMEventListener>();
        }

        ~MMEntity()
        {
            this.Dispose(true);
        }

        #endregion Destructors

        #region Events

        public event EventHandler<EventArgs> EnabledChanged;

        public event EventHandler<EventArgs> UpdateOrderChanged;

        public event EventHandler<EventArgs> ActionStopped;
        
        public event EventHandler<EventArgs> ActionStarted;

        #endregion

        #region Event Ons

        protected virtual void OnEnabledChanged(object sender, EventArgs args)
        {
            this.EnabledChanged?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnUpdateOrderChanged(object sender, EventArgs args)
        {
            this.UpdateOrderChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnActionStopped()
        {
            this.ActionStopped?.Invoke(this, EventArgs.Empty);
        }

        private void OnActionStarted()
        {
            this.ActionStarted?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Event Data

        protected List<IMMEventListener> Listeners { get; set; }

        #endregion Event Data

        #region Entity Data

        public Guid Guid { get; private set; }

        #endregion

        #region States

        private bool enabled = true;

        public virtual bool Enabled
        {
            get
            {
                return this.enabled;
            }

            set
            {
                var changed = this.enabled != value;
                if (changed)
                {
                    this.enabled = value;

                    this.OnEnabledChanged(this, null);
                }
            }
        }

        #endregion States

        #region Load and Unload

        public virtual void LoadContent()
        {
            this.Listeners.ForEach(l => this.Interop.Event.AddListener(l));
        }

        public virtual void UnloadContent()
        {
            this.Listeners.ForEach(l => this.Interop.Event.RemoveListener(l));
            this.Listeners.Clear();
        }

        #endregion Load and Unload

        #region Update

        private int updateOrder;

        public int UpdateOrder
        {
            get
            {
                return this.updateOrder;
            }

            set
            {
                if (this.updateOrder != value)
                {
                    this.updateOrder = value;

                    this.OnUpdateOrderChanged(this, null);
                }
            }
        }

        public virtual void Update(GameTime time)
        {
            this.ContinueAction();
        }

        #endregion Update

        #region Update Queue

        private List<Action> cachedActions = new List<Action>();

        private Action currentAction;

        protected void CacheAction(Action action)
        {
            this.cachedActions.Add((() => this.TriggerAction(action)));
        }

        /// <summary>
        /// Removes all actions cached.
        /// </summary>
        protected void ClearAction()
        {
            this.cachedActions.Clear();
        }

        /// <summary>
        /// Runs a single action cached.
        /// </summary>
        protected void ContinueAction()
        {
            if (this.currentAction == null
                && this.cachedActions.Count != 0)
            {
                this.currentAction = this.cachedActions.First();
                this.currentAction();
            }
        }

        /// <summary>
        /// Runs all of actions cached.
        /// </summary>
        protected void FlushAction()
        {
            if (this.currentAction == null
                && this.cachedActions.Count != 0)
            {
                foreach (var action in this.cachedActions.ToArray())
                {
                    this.currentAction = action;
                    this.currentAction();
                }
            }
        }

        protected void QueueAction(Action action)
        {
            if (this.currentAction == null)
            {
                this.currentAction = () => this.TriggerAction(action);
                this.currentAction();
            }
            else
            {
                this.CacheAction(action);
            }
        }

        private void TriggerAction(Action action)
        {
            if (this.cachedActions.Count == 0)
            {
                this.OnActionStarted();
            }

            action();

            if (this.cachedActions.Contains(this.currentAction))
            {
                this.cachedActions.Remove(this.currentAction);
            }

            this.currentAction = null;

            if (this.cachedActions.Count == 0)
            {
                this.OnActionStopped();
            }
        }

        #endregion

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

                    this.UpdateOrderChanged = null;
                    this.EnabledChanged     = null;

                    this.IsDisposed = true;
                }
            }
        }

        #endregion IDisposable
    }
}