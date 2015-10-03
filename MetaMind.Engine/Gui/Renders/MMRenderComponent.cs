namespace MetaMind.Engine.Gui.Renders
{
    using System;
    using Elements;
    using Entities;
    using Graphics.Adapters;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Node;
    using Service;
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
            // Root will be reset by Add / Remove.
            this.Root = this;

            this.ViewportAdapter = new DefaultViewportAdapter(this.GraphicsDevice);

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

        #region Dependency

        /// <summary>
        /// Delegates all the geometry related functionalities and events. 
        /// Properties of element data are provided by this.
        /// </summary>
        private MMRectangle Rectangle { get; } = new MMRectangle();

        #endregion

        #region Visual Data

        private byte opacity = byte.MaxValue;

        public Func<byte> OpacitySelector { get; set; }

        public virtual byte Opacity
        {
            get { return this.OpacitySelector?.Invoke() ?? this.opacity; }
            set
            {
                var changed = this.opacity != value;

                this.opacity = value;
                
                if (changed)
                {
                    if (this.Active)
                    {
                        this.OnOpacityChanged(this, EventArgs.Empty);
                    }
                }
            }
        }

        protected byte MixedMinOpacity(byte alpha)
        {
            return Math.Min(this.Opacity, alpha);
        }

        protected byte MixedMaxOpacity(byte alpha)
        {
            return Math.Max(this.Opacity, alpha);
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

        public virtual void Add(IMMRenderComponent component)
        {
            if (component != null)
            {
                if (!this.Children.Contains(component))
                {
                    var componentInternal = ((IMMRenderComponentInternal)component);

                    // Restore parent relationship before adding
                    componentInternal.Parent?.Remove(component);

                    // Configure parenthood
                    component.Enabled = (this.Enabled ? component.Enabled : this.Enabled);
                    componentInternal.Parent = this;
                    componentInternal.Root = this.Root;

                    // Add to children list
                    this.Children.Add(component);

                    this.Resize += componentInternal.OnParentResize;

                    if (this.Active)
                    {
                        this.OnParentChanged();
                    }
                }
            }
        }

        /// <summary>
        /// Remove existing parenthood to original state.
        /// </summary>
        /// <param name="component"></param>
        public virtual void Remove(IMMRenderComponent component)
        {
            if (component != null)
            {
                var componentInternal = ((IMMRenderComponentInternal)component);

                // Remove from children list
                this.Children.Remove(component);

                // Reconfigure parenthood to original state
                componentInternal.Parent = null;
                componentInternal.Root = component;

                this.Resize -= componentInternal.OnParentResize;

                if (this.Active)
                {
                    this.OnParentChanged();
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

        public bool Active
        {
            get { return this.Rectangle.Active; }
            set { this.Rectangle.Active = value; }
        }

        #endregion

        #region Element Data

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

        IMMRenderOpacity IMMRenderComponent.Opacity { get; set; }

        public event EventHandler<MMRenderComponentDrawEventArgs> BeginDrawStarted = delegate { };

        public event EventHandler<MMRenderComponentDrawEventArgs> EndDrawStarted = delegate { };

        public event EventHandler ParentChanged = delegate { };

        #endregion

        #region Event Ons

        public void OnOpacityChanged(object sender, EventArgs e)
        {
            // TODO(Further)
        }

        public void OnParentResize(object sender, MMElementEventArgs e)
        {
            // TODO(Further): Broadcast event from parent
        }

        public void OnParentChanged()
        {
            this.ParentChanged?.Invoke(this, EventArgs.Empty);
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
        public sealed override void BeginDraw(IMMEngineGraphicsService graphics, GameTime time, byte alpha)
        {
            base.BeginDraw(graphics, time, alpha);

            if (!this.Visible)
            {
                return;
            }

            this.CreateRenderTarget();

            this.Children.BeginDraw(graphics, time, alpha);

            this.OnBeginDrawStarted(
                this,
                new MMRenderComponentDrawEventArgs(
                    this.RenderTarget,
                    this.RenderTargetDestinationRectangle,
                    time));

            this.GraphicsDevice.SetRenderTarget(this.RenderTarget);
            this.GraphicsDevice.Clear(Color.Transparent);

            this.Draw(graphics, time, this.MixedMinOpacity(alpha));

            this.GraphicsDevice.SetRenderTarget(null);
        }

        /// <summary>
        /// Re-draw its own render target into parent's render target or back buffer.
        /// </summary>
        public sealed override void EndDraw(IMMEngineGraphicsService graphics, GameTime time, byte alpha)
        {
            base.EndDraw(graphics, time, alpha);

            if (!this.Visible)
            {
                return;
            }

            this.Children.EndDraw(graphics, time, alpha);

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
                Color.White.MakeTransparent(this.MixedMinOpacity(alpha)));
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

            base         .Update(time);
            this.Children.Update(time);
        }

        #endregion

        #region Buffer

        public override void UpdateForwardBuffer()
        {
            if (!this.Enabled)
            {
                return;
            }

            base         .UpdateForwardBuffer();
            this.Children.UpdateForwardBuffer();
        }

        public override void UpdateBackwardBuffer()
        {
            if (!this.Enabled)
            {
                return;
            }

            base         .UpdateBackwardBuffer();
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
                        this.RenderTarget.Dispose();

                        this.Children.ForEach(c => c.Dispose());
                        this.Children.Clear();
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