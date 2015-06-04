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
    using System.Runtime.Serialization;

    using MetaMind.Engine.Components.Events;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    [DataContract]
    public class GameEntity : IGameEntity
    {
        #region Event Data

        protected List<IListener> Listeners { get; set; }

        #endregion Event Data

        #region Dependency
        
        protected IGameInteropService GameInterop { get; private set; }

        protected IGameNumericalService GameNumerical { get; private set; }

        [OnDeserialized]
        private void RegisterDependency(StreamingContext context)
        {
            this.RegisterDependency();
        }

        private void RegisterDependency()
        {
            this.GameInterop = GameEngine.Service.Interop;
            this.GameNumerical = GameEngine.Service.Numerical;
        }

        #endregion 

        #region Constructors

        protected GameEntity()
        {
            this.RegisterDependency();

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
        }

        public virtual void UpdateForwardBuffer()
        {
        }

        public virtual void UpdateBackwardBuffer()
        {
        }

        #endregion Update

        #region IDisposable

        public virtual void Dispose()
        {
            this.UnloadContent(this.GameInterop);
        }

        #endregion IDisposable
    }
}