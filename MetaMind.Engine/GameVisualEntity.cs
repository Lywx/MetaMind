// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameVisualEntity.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine
{
    using System;
    using System.Runtime.Serialization;

    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    [DataContract]
    public class GameVisualEntity : GameEntity, IGameVisualEntity
    {
        #region States

        private bool visible = true;

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

        #endregion

        #region Events

        public event EventHandler<EventArgs> DrawOrderChanged;

        public event EventHandler<EventArgs> VisibleChanged;

        protected virtual void OnDrawOrderChanged(object sender, EventArgs args)
        {
        }

        protected virtual void OnVisibleChanged(object sender, EventArgs args)
        {
        }

        #endregion

        #region Dependency

        protected IGameGraphicsService GameGraphics { get; private set; }

        [OnDeserialized]
        private void RegisterDependency(StreamingContext context)
        {
            this.RegisterDependency();
        }

        private void RegisterDependency()
        {
            this.GameGraphics = GameEngine.Service.Graphics;
        }

        #endregion

        #region Constructors

        protected GameVisualEntity()
        {
            this.RegisterDependency();
        }

        #endregion

        #region Draw

        private int drawOrder;

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

        public virtual void Draw(IGameGraphicsService graphics, GameTime time, byte alpha) { }

        #endregion
    }
}