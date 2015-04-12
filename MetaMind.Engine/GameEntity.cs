// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameEntity.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine
{
    using System;

    using Microsoft.Xna.Framework;

    public class GameEntity : IUpdateable, IDisposable
    {
        #region Constructors

        public GameEntity()
        {
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

        public virtual void Update(GameTime gameTime) { }

        public virtual void Update(IGameFile gameFile, GameTime gameTime) { }

        public virtual void Update(IGameInterop gameInterop, GameTime gameTime) { }

        public virtual void Update(IGameSound gameSound, GameTime gameTime) { }

        #endregion Update

        #region IDisposable

        public virtual void Dispose()
        {
        }

        #endregion IDisposable
    }
}