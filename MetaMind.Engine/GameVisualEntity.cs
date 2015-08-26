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
    using Microsoft.Xna.Framework;
    using Services;

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
                    this.VisibleChanged?.Invoke(this, EventArgs.Empty);

                    this.OnVisibleChanged(this, EventArgs.Empty);
                }
            }
        }

        #endregion

        #region Constructors

        protected internal GameVisualEntity()
        {
            this.SetupService();
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

        #region Service

        protected IGameGraphicsService GameGraphics { get; private set; }

        [OnDeserialized]
        private void SetupService(StreamingContext context)
        {
            this.SetupService();
        }

        private void SetupService()
        {
            this.GameGraphics = GameEngine.Service?.Graphics;
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
                    this.DrawOrderChanged?.Invoke(this, null);

                    this.OnDrawOrderChanged(this, null);
                }
            }
        }

        public virtual void Draw(IGameGraphicsService graphics, GameTime time, byte alpha) { }

        #endregion

        #region IDisposable

        public override void Dispose()
        {
            base.Dispose();

            this.GameGraphics = null;
        }

        #endregion
    }
}