namespace MetaMind.Engine.Entities.Shapes
{
    using System;
    using Entities.Elements;
    using Microsoft.Xna.Framework;

    public class MMRectangle : MMInputEntity, IMMRectangle
    {
        #region Constructors and Finalizer

        public MMRectangle()
        {
            
        }

        #endregion Constructors and Destructors

        #region Events

        public event EventHandler<MMElementEventArgs> Move = delegate {};

        public event EventHandler<MMElementEventArgs> Resize = delegate {};

        #endregion

        #region Event On

        protected virtual void OnShapeMove()
        {
            this.Move?.Invoke(this, new MMElementEventArgs(MMElementEvent.Element_Move));
        }

        protected virtual void OnShapeResize()
        {
            this.Resize?.Invoke(this, new MMElementEventArgs(MMElementEvent.Element_Resize));
        }

        #endregion

        #region Element Geometry Data

        /// <remarks>
        /// Initialized to have a size of a pixel. Location is outside the screen.
        /// </remarks>>
        private Rectangle bounds = new Rectangle(int.MinValue, int.MinValue, 0, 0);

        public virtual Rectangle Bounds
        {
            get
            {
                return this.bounds;
            }

            set
            {
                var deltaLocation = this.bounds.Location.DistanceFrom(value.Location);
                var deltaSize = this.bounds.Size.DistanceFrom(value.Size);

                this.bounds = value;

                var hasMoved = deltaLocation.Length() > 0f;
                var hasResized = deltaSize.Length() > 0;

                if (hasMoved)
                {
                    this.OnShapeMove();
                }

                if (hasResized)
                {
                    this.OnShapeResize();
                }
            }
        }

        public virtual Point Center
        {
            get
            {
                return this.Bounds.Center;
            }
            set
            {
                this.Bounds = new Rectangle(value, this.Size);
            }
        }

        public virtual int Height
        {
            get
            {
                return this.Bounds.Height;
            }
            set
            {
                this.Bounds = new Rectangle(this.Bounds.X, this.Bounds.Y, this.Bounds.Width, value);
            }
        }

        public virtual Point Location
        {
            get
            {
                return this.Bounds.Location;
            }
            set
            {
                this.Bounds = new Rectangle(value.X, value.Y, this.Bounds.Width, this.Bounds.Height);
            }
        }

        public virtual Point Size
        {
            get
            {
                return new Point(this.Bounds.Width, this.Bounds.Height);
            }
            set
            {
                this.Bounds = new Microsoft.Xna.Framework.Rectangle(this.Center, value);
            }
        }

        public virtual int Width
        {
            get
            {
                return this.Bounds.Width;
            }
            set
            {
                this.Bounds = new Rectangle(this.Bounds.X, this.Bounds.Y, value, this.Bounds.Height);
            }
        }

        public virtual int X
        {
            get
            {
                return this.Bounds.X;
            }
            set
            {
                this.Bounds = new Rectangle(value, this.Bounds.Y, this.Bounds.Width, this.Bounds.Height);
            }
        }

        public virtual int Y
        {
            get
            {
                return this.Bounds.Y;
            }
            set
            {
                this.Bounds = new Rectangle(this.Bounds.X, value, this.Bounds.Width, this.Bounds.Height);
            }
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
                        this.DisposeEvents();

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

        private void DisposeEvents()
        {
            this.DisposeShapeChangeEvents();
        }

        private void DisposeShapeChangeEvents()
        {
            this.Move = null;
            this.Resize = null;
        }

        #endregion
    }
}