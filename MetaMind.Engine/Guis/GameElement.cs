// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameElement.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis
{
    using System;

    using Microsoft.Xna.Framework;

    public class GameElement : IUpdateable, IDisposable
    {
        private bool enabled = true;

        private int updateOrder;

        public event EventHandler<EventArgs> EnabledChanged;

        public event EventHandler<EventArgs> UpdateOrderChanged;

        #region Constructors

        public GameElement()
        {

        }

        #endregion

        #region Destructors

        ~GameElement()
        {
            this.Dispose();
        }

        #endregion Destructors

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

        public virtual void Update(GameTime gameTime)
        {
        }

        protected virtual void OnEnabledChanged(object sender, EventArgs args)
        {
        }

        protected virtual void OnUpdateOrderChanged(object sender, EventArgs args)
        {
        }

        #region IDisposable

        public virtual void Dispose()
        {
        }

        #endregion IDisposable
    }
}