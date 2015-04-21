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

    using MetaMind.Engine.Components.Events;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class GameEntity : IGameEntity
    {
        #region Event Data

        protected List<IListener> Listeners { get; set; }

        #endregion Event Data

        #region Engine Service
        
        protected IGameInteropService Interop { get; private set; }

        protected IGameNumericalService Numerical { get; private set; }

        #endregion 

        #region Constructors

        protected GameEntity()
        {
            this.Listeners = new List<IListener>();


            this.Interop   = GameEngine.Service.Interop;
            this.Numerical = GameEngine.Service.Numerical;
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
        }

        public virtual void LoadGraphics(IGameGraphicsService graphics)
        {
        }

        public virtual void LoadInterop(IGameInteropService interop)
        {
            this.Listeners.ForEach(l => interop.Event.AddListener(l));
        }

        public virtual void UnloadContent(IGameInteropService interop)
        {
        }

        public virtual void UnloadGraphics(IGameGraphicsService graphics)
        {
        }

        public virtual void UnloadInterop(Services.IGameInteropService interop)
        {
            // TODO: UnloadInterop How to design
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

        public virtual void UpdateInterop(IGameInteropService interop, GameTime time)
        {
        }

        #endregion Update

        #region IDisposable

        public virtual void Dispose()
        {
            this.UnloadInterop(this.Interop);
        }

        #endregion IDisposable
    }
}