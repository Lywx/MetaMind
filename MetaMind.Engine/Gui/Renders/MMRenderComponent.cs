namespace MetaMind.Engine.Gui.Renders
{
    using System;
    using Elements;
    using Entities;
    using Graphics.Adapters;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Nodes;
    using Services;
    using Shapes;

    public abstract class MMRenderComponent : MMVisualEntity, IMMRenderComponent, IMMRenderComponentInternal
    {
        #region Constructors and Finalizer

        protected MMRenderComponent()
        {
            this.Constructor();
        }

        private void Constructor()
        {
            // Engine data
            this.ViewportAdapter = new DefaultViewportAdapter(this.GraphicsDevice);

            // Visual data
            this.Opacity = new MMRenderOpacity(this);

            // Structural data
            this.Root = this;

            // Element data
            
            // Re-broadcast events from Rectangle.
            this.Rectangle.Move += (sender, args) =>
            {
                this.Move?.Invoke(sender, args);
            };

            this.Rectangle.Resize += (sender, args) =>
            {
                this.Resize?.Invoke(sender, args);
            };
        }

        #endregion

        #region Engine Data

        protected GraphicsDevice GraphicsDevice => this.Graphics.GraphicsDevice;

        protected SpriteBatch SpriteBatch => this.Graphics.SpriteBatch;

        public ViewportAdapter ViewportAdapter { get; set; }

        protected Viewport Viewport => this.GraphicsDevice.Viewport;

        #endregion

        #region Visual Data

        public IMMRenderOpacity Opacity { get; protected set; }

        private int zorder;

        public int ZOrder
        {
            get { return this.zorder; }
            set
            {
                if (this.zorder != value)
                {
                    if (this.Parent != null)
                    {
                        this.Parent.ReorderChild(this, value);
                    }

                    this.zorder = value;
                }
            }
        }

        #endregion

        #region Structural Data

        public MMRenderComponenetCollection Children { get; protected set; } = new MMRenderComponenetCollection();

        public IMMRenderComponent Parent { get; set; } = null;

        public IMMRenderComponent Root { get; set; } = null;

        /// <summary>
        /// Gets a value indicating whether this control is a child control.
        /// </summary>
        public virtual bool IsChild => this.Parent != null;

        /// <summary>
        /// Gets a value indicating whether this control is a parent control.
        /// </summary>
        public virtual bool IsParent => this.Children != null && this.Children.Count > 0;

        /// <summary>
        /// Gets a value indicating whether this control is a root control.
        /// </summary>
        public virtual bool IsRoot => this.Root == this;

        public virtual void Add(IMMRenderComponentInternal component)
        {
            if (component != null)
            {
                if (!this.Children.Contains(component as IMMRenderComponent))
                {
                    // Restore parent relationship before adding
                    component.Parent?.Remove(component);

                    // Configure parenthood
                    component.Enabled = this.Enabled ? component.Enabled : this.Enabled;
                    component.Parent = this;
                    component.Root = this.Root;

                    // Add to children list
                    this.Children.Add(component as IMMRenderComponent);

                    this.Resize += component.OnParentResize;

                    if (this.Enabled)
                    {
                        this.OnParentChanged(this, EventArgs.Empty);
                    }
                }
            }
        }

        /// <summary>
        /// Remove existing parenthood to original state.
        /// </summary>
        /// <param name="component"></param>
        public virtual void Remove(IMMRenderComponentInternal component)
        {
            if (component != null)
            {
                // Remove from children list
                this.Children.Remove(component as IMMRenderComponent);

                // Reconfigure parenthood to original state
                component.Parent = null;
                component.Root = component as IMMRenderComponent;

                this.Resize -= component.OnParentResize;

                if (this.Enabled)
                {
                    this.OnParentChanged(this, EventArgs.Empty);
                }
            }
        }

        public virtual bool Contains(IMMRenderComponent component, bool recursive)
        {
            if (this.Children != null)
            {
                foreach (var c in this.Children)
                {
                    if (c == component)
                    {
                        return true;
                    }

                    if (recursive && c.Contains(component, true))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        #endregion

        #region State Data

        public override bool Enabled
        {
            get { return this.Rectangle.Enabled; }
            set { this.Rectangle.Enabled = value; }
        }

        #endregion

        #region Element Data

        /// <summary>
        /// Delegates all the geometry related functionalities and events. 
        /// Properties of element data are provided by this.
        /// </summary>
        private MMRectangle Rectangle { get; } = new MMRectangle();

        public event EventHandler<MMElementEventArgs> Move = delegate {};

        public event EventHandler<MMElementEventArgs> Resize = delegate {};

        public virtual Point Size
        {
            get { return this.Rectangle.Size; }
            set { this.Rectangle.Size = value; }
        }

        public virtual int Width
        {
            get { return this.Rectangle.Width; }
            set { this.Rectangle.Width = value; }
        }

        public virtual int Height
        {
            get { return this.Rectangle.Height; }
            set { this.Rectangle.Height = value; }
        }

        public virtual Rectangle Bounds
        {
            get { return this.Rectangle.Bounds; }
            set { this.Rectangle.Bounds = value; }
        }

        public virtual Point Center
        {
            get { return this.Rectangle.Center; }
            set { this.Rectangle.Center = value; }
        }

        public virtual Point Location
        {
            get { return this.Rectangle.Location; }
            set { this.Rectangle.Location = value; }
        }

        public virtual int X
        {
            get { return this.Rectangle.X; }
            set { this.Rectangle.X = value; }
        }

        public virtual int Y
        {
            get { return this.Rectangle.Y; }
            set { this.Rectangle.Y = value; }
        }

        #endregion

        #region Render Data

        public RenderTarget2D RenderTarget { get; set; }

        protected virtual int RenderTargetWidth
        {
            get
            {
                var w = this.Width;

                if (w > this.Viewport.Width)
                {
                    w = this.Viewport.Width;
                }

                return w;
            }
        }

        protected virtual int RenderTargetHeight
        {
            get
            {
                var h = this.Height;

                if (h > this.Viewport.Height)
                {
                    h = this.Viewport.Height;
                }

                return h;
            }
        }

        protected Point RenderTargetSize => new Point(this.RenderTargetWidth, this.RenderTargetHeight);

        protected Rectangle RenderTargetDestinationRectangle => new Rectangle(this.Rectangle.Location, this.RenderTargetSize);

        protected Rectangle RenderTargetSourceRectangle => new Rectangle(Point.Zero, this.RenderTargetSize);

        private void CreateRenderTarget()
        {
            if (this.RenderTarget == null ||
                this.RenderTarget.Width != this.RenderTargetWidth ||
                this.RenderTarget.Height != this.RenderTargetHeight)
            {
                if (this.RenderTarget != null)
                {
                    this.RenderTarget.Dispose();
                    this.RenderTarget = null;
                }

                this.RenderTarget =
                    MMRenderTargetFactory.Create(this.RenderTargetSize);
            }
        }

        protected virtual void SetBackRenderTarget()
        {
            // Set to parent's render target when it has parent.
            this.GraphicsDevice.SetRenderTarget(
                this.Parent.RenderTarget);

            this.GraphicsDevice.
            this.GraphicsDevice.RestoreRenderTarget();
        }

        #endregion

        #region Events

        public event EventHandler<MMRenderComponentDrawEventArgs> BeginDrawStarted = delegate { };

        public event EventHandler<MMRenderComponentDrawEventArgs> EndDrawStarted = delegate { };

        public event EventHandler ParentChanged = delegate { };

        #endregion

        #region Event Ons

        public void OnOpacityChanged(object sender, EventArgs e)
        {
            // TODO(Further): 
        }

        public void OnParentResize(object sender, MMElementEventArgs e)
        {
            // TODO(Further): Broadcast event from parent
        }

        public void OnParentChanged(object sender, EventArgs e)
        {
            this.ParentChanged?.Invoke(this, e);
        }

        public void OnBeginDrawStarted(object sender, MMRenderComponentDrawEventArgs e)
        {
            this.BeginDrawStarted?.Invoke(sender, e);
        }

        public void OnEndDrawStarted(object sender, MMRenderComponentDrawEventArgs e)
        {
            this.EndDrawStarted?.Invoke(sender, e);
        }

        #endregion

        #region Comparison

        public int CompareTo(IMMRenderComponent other)
        {
            return other.ZOrder.CompareTo(other.ZOrder);
        }

        #endregion

        #region Initialization

        private bool PositionInitialized => this.Rectangle.Location.X > int.MinValue / 2 &&
                                            this.Rectangle.Location.Y > int.MinValue / 2;

        public bool Initialized { get; private set; }

        public void Initialize()
        {
            if (!this.PositionInitialized)
            {
                throw new InvalidOperationException();
            }

            this.Initialized = true;
        }

        #endregion

        #region Draw

        /// <summary>
        /// Configure drawing in its own render target. 
        /// </summary>
        /// <remarks>
        /// This is capable to process non-controls children elements by
        /// overriding this.Draw method, in which case, all elements are drawn 
        /// to parent control's render target.
        /// </remarks>
        public sealed override void BeginDraw(IMMEngineGraphicsService graphics, GameTime time)
        {
            base.BeginDraw(graphics, time);

            if (!this.Visible)
            {
                return;
            }

            this.CreateRenderTarget();

            this.Children.BeginDraw(graphics, time);

            this.OnBeginDrawStarted(
                this,
                new MMRenderComponentDrawEventArgs(
                    this.RenderTarget,
                    this.RenderTargetDestinationRectangle,
                    time));

            this.GraphicsDevice.SetRenderTarget(this.RenderTarget);
            this.GraphicsDevice.Clear(Color.Transparent);

            this.Draw(graphics, time);

            this.GraphicsDevice.SetRenderTarget(null);
        }

        /// <summary>
        /// Re-draw its own render target into parent's render target or back buffer.
        /// </summary>
        public sealed override void EndDraw(IMMEngineGraphicsService graphics, GameTime time)
        {
            base.EndDraw(graphics, time);

            if (!this.Visible)
            {
                return;
            }

            this.Children.EndDraw(graphics, time);

            this.OnEndDrawStarted(
                this,
                new MMRenderComponentDrawEventArgs(
                    this.RenderTarget,
                    this.RenderTargetDestinationRectangle,
                    time));

            if (this.IsChild)
            {
                this.SetBackRenderTarget();
            }

            this.SpriteBatch.Begin();
            this.SpriteBatch.Draw(
                this.RenderTarget,
                this.RenderTargetDestinationRectangle,
                this.RenderTargetSourceRectangle, 
                Color.White);
            this.SpriteBatch.End();
        }

        #endregion

        #region Update

        public override void Update(GameTime time)
        {
            if (!this.Enabled)
            {
                return;
            }

            this.Children.Update(time);
        }

        #endregion

        #region Buffer

        public void UpdateForwardBuffer()
        {
            if (!this.Enabled)
            {
                return;
            }

            this.Children.UpdateForwardBuffer();
        }

        public void UpdateBackwardBuffer()
        {
            if (!this.Enabled)
            {
                return;
            }

            this.Children.UpdateBackwardBuffer();
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
                        this.DisposeRenderTarget();
                        this.DisposeChildren();
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

        private void DisposeRenderTarget()
        {
            this.RenderTarget?.Dispose();
        }

        private void DisposeChildren()
        {
            foreach (var component in this.Children)
            {
                component.Dispose();
            }

            this.Children.Clear();
        }

        #endregion
    }
}