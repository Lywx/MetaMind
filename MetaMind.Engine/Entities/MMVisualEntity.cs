namespace MetaMind.Engine.Entities
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

        #region Comparison

        public int Compare(IMMDrawable x, IMMDrawable y)
        {
            return x.CompareTo(y);
        }

        public int CompareTo(IMMDrawable other)
        {
            return this.DrawOrder.CompareTo(other.DrawOrder);
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
                var changed = this.drawOrder != value;
                if (changed)
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

        public event EventHandler<EventArgs> DrawOrderChanged;

        public event EventHandler<EventArgs> VisibleChanged;

        #endregion

        #region Event Ons

        protected virtual void OnDrawOrderChanged(object sender, EventArgs args)
        {
        }

        protected virtual void OnVisibleChanged(object sender, EventArgs args)
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