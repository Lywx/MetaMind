namespace MetaMind.Engine.Entities.Graphics
{
    using Adapters;
    using Components.Graphics;
    using Elements;
    using Entities;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Shapes;
    using System;

    public abstract class MMRendererComponent : MMVisualEntity,
        IMMRendererComponent,
        IMMRendererComponentInternal
    {
        #region Constructors and Finalizer

        protected MMRendererComponent()
        {
            // Visual data
            this.Opacity = new MMRendererOpacity(this);

            // Structural data
            this.Root = this;

            // Element data
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

        protected MMViewportAdapter ViewportAdapter { get; set; } =
            new MMDefaultViewportAdapter();

        protected Viewport Viewport => this.GraphicsDevice.Viewport;

        #endregion

        #region Visual Data

        public bool CascadedEnabled { get; set; } = false;

        // TODO: Haven't been added
        public CCClipMode ClipMode { get; set; } = CCClipMode.None;

        public IMMRendererOpacity Opacity { get; protected set; }

        public int ZOrder
        {
            get { return this.DrawOrder; }
            set { this.DrawOrder = value; }
        }

        #endregion

        #region Structural Data

        public MMRendererComponenetCollection Children { get; protected set; } =
            new MMRendererComponenetCollection();

        public IMMRendererComponent Parent { get; set; } = null;

        public IMMRendererComponent Root { get; set; } = null;

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

        public virtual void Add(IMMRendererComponent component)
        {
            if (component != null)
            {
                if (!this.Children.Contains(component))
                {
                    // Restore parent relationship before adding
                    component.Parent?.Remove(component);

                    // Configure parenthood
                    component.Enabled = this.Enabled ? component.Enabled : this.Enabled;

                    var componentInternal = (IMMRendererComponentInternal)component;
                    componentInternal.Parent = this;
                    componentInternal.Root = this.Root;

                    // Add to children list
                    this.Children.Add(component);

                    this.Resize += componentInternal.OnParentResize;

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
        public virtual void Remove(IMMRendererComponent component)
        {
            if (component != null)
            {
                // Remove from children list
                this.Children.Remove(component);

                // Reconfigure parenthood to original state
                var componentInternal = (IMMRendererComponentInternal)component;
                componentInternal.Parent = null;
                componentInternal.Root = component;

                this.Resize -= componentInternal.OnParentResize;

                if (this.Enabled)
                {
                    this.OnParentChanged(this, EventArgs.Empty);
                }
            }
        }

        public virtual bool Contains(IMMRendererComponent component, bool recursive)
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

        public event EventHandler<MMElementEventArgs> Move = delegate { };

        public event EventHandler<MMElementEventArgs> Resize = delegate { };

        /// <summary>
        /// Delegates all the geometry related functionalities and events.
        /// Properties of element data are provided by this.
        /// </summary>
        private MMRectangle Rectangle { get; } = new MMRectangle();

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

        protected Point RenderTargetSize
            => new Point(this.RenderTargetWidth, this.RenderTargetHeight);

        protected Rectangle RenderTargetDestinationRectangle
            => new Rectangle(this.Rectangle.Location, this.RenderTargetSize);

        protected Rectangle RenderTargetSourceRectangle
            => new Rectangle(Point.Zero, this.RenderTargetSize);

        protected void CreateRenderTarget()
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

        protected virtual void SetParentRenderTarget()
        {
            this.GraphicsDevice.SetRenderTarget(this.Parent.RenderTarget);
        }

        #endregion

        #region Events

        public event EventHandler<MMRendererComponentDrawEventArgs>
            CascadedBeginDrawStarted = delegate {};

        public event EventHandler<MMRendererComponentDrawEventArgs>
            CascadedEndDrawStarted = delegate {};

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

        public void OnCascadedBeginDrawStarted(
            object sender,
            MMRendererComponentDrawEventArgs e)
        {
            this.CascadedBeginDrawStarted?.Invoke(sender, e);
        }

        public void OnCascadedEndDrawStarted(
            object sender,
            MMRendererComponentDrawEventArgs e)
        {
            this.CascadedEndDrawStarted?.Invoke(sender, e);
        }

        #endregion

        #region Comparison

        public int CompareTo(IMMRendererComponent other)
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
        /// Begin drawing in its own render target.
        /// </summary>
        /// <remarks>
        /// GraphicsDevice's RenderTarget will be changed inside BeginDraw.
        /// However, it will be restore to original state after the method.
        ///
        /// This is capable to process non-controls children elements by
        /// overriding this.Draw method, in which case, all elements are drawn
        /// to parent control's render target.
        /// </remarks>
        public sealed override void BeginDraw(GameTime time)
        {
            if (!this.Visible)
            {
                return;
            }

            // Draw to back buffer directly
            if (!this.CascadedEnabled)
            {
                this.Draw(time);
            }

            // When cascaded
            else
            {
                this.CreateRenderTarget();

                this.Children.BeginDraw(time);

                this.OnCascadedBeginDrawStarted(
                    this,
                    new MMRendererComponentDrawEventArgs(
                        this.RenderTarget,
                        this.RenderTargetDestinationRectangle,
                        time));

                this.GraphicsDevice.SetRenderTarget(this.RenderTarget);
                this.GraphicsDevice.Clear(Color.Transparent);

                this.Draw(time);

                this.GraphicsDeviceController.RestoreRenderTarget();
            }
        }

        /// <summary>
        /// Signal the end of all the drawing work. It will re-draw its own
        /// render target into parent's render target or back buffer.
        /// </summary>
        public sealed override void EndDraw(GameTime time)
        {
            if (!this.Visible)
            {
                return;
            }

            // Only redraw when cascaded.
            if (!this.CascadedEnabled)
            {
                return;
            }

            this.Children.EndDraw(time);

            this.OnCascadedEndDrawStarted(
                this,
                new MMRendererComponentDrawEventArgs(
                    this.RenderTarget,
                    this.RenderTargetDestinationRectangle,
                    time));

            if (this.IsChild)
            {
                this.SetParentRenderTarget();
            }

            this.GraphicsRenderer.DrawImmediate(
                this.RenderTarget,
                this.RenderTargetDestinationRectangle,
                this.RenderTargetSourceRectangle,
                Color.White);
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