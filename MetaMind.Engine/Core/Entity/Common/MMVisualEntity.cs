namespace MetaMind.Engine.Core.Entity.Common
{
    using System;
    using System.Runtime.Serialization;
    using Microsoft.Xna.Framework;

    [DataContract]
    public class MMVisualEntity : MMEntity, IMMVisualEntity
    {
        #region Constructors

        protected MMVisualEntity()
        {
        }

        ~MMVisualEntity()
        {
            this.Dispose(true);
        }

        #endregion

        #region State Data

        private bool entityVisible = true;

        public virtual bool EntityVisible
        {
            get
            {
                return this.entityVisible;
            }

            set
            {
                if (this.entityVisible != value)
                {
                    this.entityVisible = value;
                    this.EntityVisibleChanged?.Invoke(this, EventArgs.Empty);

                    this.OnEntityVisibleChanged(this, EventArgs.Empty);
                }
            }
        }

        #endregion

        #region Comparison

        public int CompareTo(IMMDrawable other)
        {
            return this.EntityDrawOrder.CompareTo(other.EntityDrawOrder);
        }

        #endregion

        #region Draw

        private int entityDrawOrder;

        /// <summary>
        /// Z Order.
        /// </summary>
        public int EntityDrawOrder
        {
            get
            {
                return this.entityDrawOrder;
            }

            set
            {
                var changed = this.entityDrawOrder != value;
                if (changed)
                {
                    this.entityDrawOrder = value;
                    this.EntityDrawOrderChanged?.Invoke(this, null);

                    this.OnEntityDrawOrderChanged(this, null);
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
        /// <param name="time"></param>
        public virtual void BeginDraw(GameTime time) { }

        /// <summary>
        /// Standard draw routine.
        /// </summary>
        /// <remarks>
        /// It is recommended not to call SpriteBatch.Begin and SpriteBatch.End 
        /// in this method and its override version.
        /// </remarks>>
        /// <param name="time"></param>
        public virtual void Draw(GameTime time) { }

        /// <summary>
        /// Standard draw termination.
        /// </summary>
        /// <remarks>
        /// You should draw RenderTarget2D and RenderTarget3D to back buffer in 
        /// this method and its override method.
        /// </remarks>>
        /// <param name="time"></param>
        public virtual void EndDraw(GameTime time) { }

        #endregion

        #region Events

        public event EventHandler<EventArgs> EntityDrawOrderChanged;

        public event EventHandler<EventArgs> EntityVisibleChanged;

        #endregion

        #region Event Ons

        protected virtual void OnEntityDrawOrderChanged(object sender, EventArgs args)
        {
        }

        protected virtual void OnEntityVisibleChanged(object sender, EventArgs args)
        {
        }

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
                        this.EntityDrawOrderChanged = null;
                        this.EntityVisibleChanged   = null;
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