// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FrameEntity.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Gui.Elements.Rectangles
{
    using System;
    using Microsoft.Xna.Framework;

    public abstract class RectangleElement : GameControllableEntity, IElement
    {
        #region Constructors and Finalizer

        protected RectangleElement()
        {
        }

        #endregion Constructors and Destructors

        public event EventHandler<ElementEventArgs> Move = delegate {};

        public event EventHandler<ElementEventArgs> Resize = delegate {};

        #region Event On

        protected virtual void OnElementMove()
        {
            this.Move?.Invoke(this, new ElementEventArgs(ElementEvent.Element_Move));
        }

        protected virtual void OnElementResize()
        {
            this.Resize?.Invoke(this, new ElementEventArgs(ElementEvent.Element_Resize));
        }

        #endregion

        #region Element State Data

        public virtual bool Active { get; set; } = true;

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
                    this.OnElementMove();
                }

                if (hasResized)
                {
                    this.OnElementResize();
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
                this.Bounds = new Rectangle(this.Center, value);
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
            this.DisposeElementChangeEvents();
        }

        private void DisposeElementChangeEvents()
        {
            this.Move = null;
            this.Resize = null;
        }

        #endregion
    }
}