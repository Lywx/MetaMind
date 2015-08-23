// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameEntity.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using Components.Events;
    using Services;
    using Microsoft.Xna.Framework;

    [DataContract]
    public class GameEntity : IGameEntity
    {
        #region Entity Data

        public Guid Guid { get; private set; }

        #endregion

        #region Event Data

        protected List<IListener> Listeners { get; set; }

        #endregion Event Data

        #region Dependency
        
        protected internal IGameInteropService GameInterop { get; private set; }

        protected internal IGameNumericalService GameNumerical { get; private set; }

        [OnDeserialized]
        private void SetupService(StreamingContext context)
        {
            this.SetupService();
        }

        private void SetupService()
        {
            if (GameEngine.Service != null)
            {
                this.GameInterop = GameEngine.Service.Interop;
                this.GameNumerical = GameEngine.Service.Numerical;
            }
        }

        #endregion 

        #region Constructors

        protected internal GameEntity()
        {
            this.SetupService();

            this.Guid = Guid.NewGuid();

            this.Listeners = new List<IListener>();
        }

        #endregion Constructors

        #region Destructors

        ~GameEntity()
        {
            this.Dispose();
        }

        #endregion Destructors

        #region Events

        public event EventHandler<EventArgs> EnabledChanged;

        public event EventHandler<EventArgs> UpdateOrderChanged;

        protected virtual void OnEnabledChanged(object sender, EventArgs args)
        {
        }

        protected virtual void OnUpdateOrderChanged(object sender, EventArgs args)
        {
        }

        #endregion Events

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
                    if (this.EnabledChanged != null)
                    {
                        this.EnabledChanged(this, EventArgs.Empty);
                    }

                    this.OnEnabledChanged(this, null);
                }
            }
        }

        #endregion States

        #region Load and Unload

        public virtual void LoadContent(IGameInteropService interop)
        {
            this.Listeners.ForEach(l => interop.Event.AddListener(l));
        }

        public virtual void UnloadContent(IGameInteropService interop)
        {
            // Maybe disposed already
            if (this.Listeners != null)
            {
                this.Listeners.ForEach(l => interop.Event.RemoveListener(l));
                this.Listeners.Clear();
            }
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
                    if (this.UpdateOrderChanged != null)
                    {
                        this.UpdateOrderChanged(this, EventArgs.Empty);
                    }

                    this.OnUpdateOrderChanged(this, null);
                }
            }
        }

        public virtual void Update(GameTime time)
        {
            this.ContinueAction(time);
        }

        public virtual void UpdateForwardBuffer()
        {
        }

        public virtual void UpdateBackwardBuffer()
        {
        }

        #endregion Update

        #region Update  Queue

        private Action updateAction;

        private readonly List<Action> updateActions = new List<Action>();

        private void OnActionStopped()
        {
        }

        private void OnActionStarted()
        {
        }

        protected void ContinueAction(GameTime time)
        {
            if (this.updateAction == null &&
                this.updateActions.Count != 0)
            {
                this.updateAction = this.updateActions.First();
                this.updateAction();
            }
        }

        protected void ClearAction(GameTime time)
        {
            this.updateActions.Clear();
        }

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

        protected void ProcessAction(Action action)
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

        public virtual void Dispose()
        {
            this.UnloadContent(this.GameInterop);
        }

        #endregion IDisposable
    }
}