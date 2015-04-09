// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DrawableGameElement.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis
{
    using System;

    using Microsoft.Xna.Framework;

    public class DrawableGameElement : GameElement, IDrawable
    {
        private int drawOrder;

        private bool visible = true;

        public event EventHandler<EventArgs> DrawOrderChanged;

        public event EventHandler<EventArgs> VisibleChanged;

        public int DrawOrder
        {
            get
            {
                return this.drawOrder;
            }

            set
            {
                if (this.drawOrder != value)
                {
                    this.drawOrder = value;
                    if (this.DrawOrderChanged != null)
                    {
                        this.DrawOrderChanged(this, null);
                    }

                    this.OnDrawOrderChanged(this, null);
                }
            }
        }

        public bool Visible
        {
            get
            {
                return this.visible;
            }

            set
            {
                if (this.visible != value)
                {
                    this.visible = value;
                    if (this.VisibleChanged != null)
                    {
                        this.VisibleChanged(this, EventArgs.Empty);
                    }

                    this.OnVisibleChanged(this, EventArgs.Empty);
                }
            }
        }

        public virtual void Draw(GameTime gameTime)
        {
        }

        protected virtual void OnDrawOrderChanged(object sender, EventArgs args)
        {
        }

        protected virtual void OnVisibleChanged(object sender, EventArgs args)
        {
        }
    }
}