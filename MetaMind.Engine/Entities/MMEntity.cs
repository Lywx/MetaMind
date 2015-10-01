// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameEntity.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using Components.Interop.Event;
    using Microsoft.Xna.Framework;
    using Service;

    [DataContract]
    public class MMEntity : MMObject, IMMEntity
    {
        #region Constructors and Finalizer 

        protected internal MMEntity()
        {
            this.Guid      = Guid.NewGuid();
            this.Listeners = new List<IListener>();
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

        #region Event On

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

        protected List<IListener> Listeners { get; set; }

        #endregion Event Data

        #region Entity Data

        public Guid Guid { get; private set; }

        #endregion

        #region States

        private bool enabled = true;

        public bool Enabled
        {
            get
            {
                return this.enabled;
            }

            set
            {
                if (this.enabled != value)
                {
                    this.enabled = value;

                    this.OnEnabledChanged(this, null);
                }
            }
        }

        #endregion States

        #region Load and Unload

        public virtual void LoadContent(IMMEngineInteropService interop)
        {
            this.Listeners.ForEach(l => interop.Event.AddListener(l));
        }

        public virtual void UnloadContent(IMMEngineInteropService interop)
        {
            this.Listeners.ForEach(l => interop.Event.RemoveListener(l));
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
            this.ContinueAction(time);
        }

        #endregion Update

        #region Update Buffer

        public virtual void UpdateForwardBuffer()
        {
        }

        public virtual void UpdateBackwardBuffer()
        {
        }

        #endregion

        #region Update Queue

        private Action updateAction;

        private readonly List<Action> updateActions = new List<Action>();

        /// <summary>
        /// Runs a single action cached.
        /// </summary>
        /// <param name="time"></param>
        protected void ContinueAction(GameTime time)
        {
            if (this.updateAction == null &&
                this.updateActions.Count != 0)
            {
                this.updateAction = this.updateActions.First();
                this.updateAction();
            }
        }

        /// <summary>
        /// Removes all actions cached.
        /// </summary>
        /// <param name="time"></param>
        protected void ClearAction(GameTime time)
        {
            this.updateActions.Clear();
        }

        /// <summary>
        /// Runs all of actions cached.
        /// </summary>
        /// <param name="time"></param>
        protected void FlushAction(GameTime time)
        {
            if (this.updateAction == null &&
                this.updateActions.Count != 0)
            {
                foreach (var action in this.updateActions.ToArray())
                {
                    this.updateAction = action;
                    this.updateAction();
                }
            }
        }

        protected void DeferAction(Action action)
        {
            this.updateActions.Add((() => this.ProcessAction(action)));
        }

        protected void StartAction(Action action)
        {
            if (this.updateAction == null)
            {
                this.updateAction = () => this.ProcessAction(action);
                this.updateAction();
            }
            else
            {
                this.DeferAction(action);
            }
        }

        private void ProcessAction(Action action)
        {
            if (this.updateActions.Count == 0)
            {
                this.OnActionStarted();
            }

            action();

            if (this.updateActions.Contains(this.updateAction))
            {
                this.updateActions.Remove(this.updateAction);
            }

            this.updateAction = null;

            if (this.updateActions.Count == 0)
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
                    this.UnloadContent(this.Interop);

                    this.UpdateOrderChanged = null;
                    this.EnabledChanged     = null;

                    this.IsDisposed = true;
                }
            }
        }

        #endregion IDisposable
    }
}