// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameVisualEntity.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine
{
    using System;
    using System.Runtime.Serialization;
    using Microsoft.Xna.Framework;
    using Service;

    [DataContract]
    public class GameVisualEntity : GameEntity, IGameVisualEntity
    {
        #region States

        private bool visible = true;

        public virtual bool Visible
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

        protected GameVisualEntity()
        {
        }

        ~GameVisualEntity()
        {
            this.Dispose(true);
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

        /// <summary>
        /// Standard draw preparation. 
        /// </summary>
        /// <remarks>
        /// You should set up to RenderTarget2D and RenderTarget3D class in this 
        /// method.
        /// </remarks>
        /// <param name="graphics"></param>
        /// <param name="time"></param>
        /// <param name="alpha"></param>
        public virtual void BeginDraw(IGameGraphicsService graphics, GameTime time, byte alpha) { }

        /// <summary>
        /// Standard draw routine.
        /// </summary>
        /// <remarks>
        /// It is recommended not to call SpriteBatch.Begin and SpriteBatch.End 
        /// in this method and its override version.
        /// </remarks>>
        /// <param name="graphics"></param>
        /// <param name="time"></param>
        /// <param name="alpha"></param>
        public virtual void Draw(IGameGraphicsService graphics, GameTime time, byte alpha) { }

        /// <summary>
        /// Standard draw termination.
        /// </summary>
        /// <remarks>
        /// You should draw RenderTarget2D and RenderTarget3D to back buffer in 
        /// this method and its override method.
        /// </remarks>>
        /// <param name="graphics"></param>
        /// <param name="time"></param>
        /// <param name="alpha"></param>
        public virtual void EndDraw(IGameGraphicsService graphics, GameTime time, byte alpha) { }

        #endregion

        #region IDisposable

        private bool IsDisposed { get; set; }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    if (!this.IsDisposed)
                    {
                        this.DrawOrderChanged = null;
                        this.VisibleChanged   = null;
                    }

                    this.IsDisposed = true;
                }
            }
            catch
            {
                // Ignored
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        #endregion
    }
}