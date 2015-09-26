namespace MetaMind.Engine.Gui.Components
{
    using System;
    using Engine.Components.Graphics.Adapters;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using MonoGame.Extended.ViewportAdapters;
    using Service;

    /// <summary>
    /// It is the basic unit of Gui rendering. It has the basic geometrical 
    /// property. But it doesn't have input related methods.
    /// </summary>
    public abstract class RenderComponent : Component, IRenderComponent
    {

        #region Constructors and Finalizer

        protected RenderComponent()
        {
            this.Constructor();
        }

        private void Constructor()
        {
            this.ViewportAdapter = new DefaultViewportAdapter(this.GraphicsDevice);
        }

        #endregion

        #region Visual Data

        private byte alpha = byte.MaxValue;

        public Func<byte> AlphaSelector { get; set; }

        public virtual byte Alpha
        {
            get { return this.AlphaSelector?.Invoke() ?? this.alpha; }
            set
            {
                var changed = this.alpha != value;

                this.alpha = value;
                
                if (changed)
                {
                    if (this.Active)
                    {
                        this.OnAlphaChanged(EventArgs.Empty);
                    }
                }
            }
        }

        protected byte MixedAlpha(byte alpha)
        {
            return Math.Min(this.Alpha, alpha);
        }

        #endregion

        #region Structural Data

        public string Name { get; protected set; } = "Render Component";

        public RenderComponent Parent { get; protected set; } = null;

        public RenderComponent Root { get; protected set; } = null;

        public GameEntityCollection<RenderComponent> Children { get; protected set; } = new GameEntityCollection<RenderComponent>();

        /// <summary>
        /// Gets a value indicating whether this control is a child control.
        /// </summary>
        public virtual bool IsChild => (this.Parent != null);

        /// <summary>
        /// Gets a value indicating whether this control is a parent control.
        /// </summary>
        public virtual bool IsParent => (this.Children != null && this.Children.Count > 0);

        /// <summary>
        /// Gets a value indicating whether this control is a root control.
        /// </summary>
        public virtual bool IsRoot => (this.Root == this);

        public virtual void Add(RenderComponent control)
        {
            if (control != null)
            {
                if (!this.Children.Contains(control))
                {
                    control.Parent?.Remove(control);

                    control.Parent = this;
                    control.Root = this.Root;
                    control.Enabled = (this.Enabled ? control.Enabled : this.Enabled);
                    this.Children.Add(control);

                    this.Resize += control.OnParentResize;

                    if (this.Active)
                    {
                        this.OnParentChanged();
                    }
                }
            }
        }

        public virtual void Remove(RenderComponent component)
        {
            if (component != null)
            {
                this.Children.Remove(component);

                component.Parent = null;
                component.Root = component;

                this.Resize -= component.OnParentResize;

                if (this.Active)
                {
                    this.OnParentChanged();
                }
            }
        }

        public virtual bool Contains(RenderComponent control, bool recursively)
        {
            if (this.Children != null)
            {
                foreach (var c in this.Children)
                {
                    if (c == control)
                    {
                        return true;
                    }

                    if (recursively && c.Contains(control, true))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        #endregion

        #region Engine Data

        protected GraphicsDevice GraphicsDevice => this.Graphics.GraphicsDevice;

        protected SpriteBatch SpriteBatch => this.Graphics.SpriteBatch;

        protected Viewport Viewport => this.GraphicsDevice.Viewport;

        protected ViewportAdapter ViewportAdapter { get; set; }

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

        protected Rectangle RenderTargetDestinationRectangle => new Rectangle(this.Location, this.RenderTargetSize);

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
                    RenderTargetFactory.Create(this.RenderTargetSize);
            }
        }

        protected virtual void SetBackRenderTarget()
        {
            // Set to parent's render target when it has parent.
            this.GraphicsDevice.SetRenderTarget(
                this.Parent.RenderTarget);
        }

        #endregion

        #region Events

        public event EventHandler<RenderComponentDrawEventArgs> BeginDrawStarted = delegate { };

        public event EventHandler<RenderComponentDrawEventArgs> EndDrawStarted = delegate { };

        public event EventHandler ParentChanged = delegate { };

        #endregion

        #region Event On

        private void OnAlphaChanged(EventArgs e)
        {
        }

        private void OnBeginDrawStarted(RenderComponentDrawEventArgs e)
        {
            this.BeginDrawStarted?.Invoke(this, e);
        }

        private void OnEndDrawStarted(RenderComponentDrawEventArgs e)
        {
            this.EndDrawStarted?.Invoke(this, e);
        }

        private void OnParentResize(object sender, EventArgs e)
        {
            // TODO(Further)
        }

        private void OnParentChanged()
        {
            this.ParentChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Initialization

        private bool PositionInitialized => this.Location.X > int.MinValue / 2 &&
                                            this.Location.Y > int.MinValue / 2;

        public override void Initialize()
        {
            if (!this.PositionInitialized)
            {
                throw new InvalidOperationException();
            }

            base.Initialize();
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
        public sealed override void BeginDraw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            base.BeginDraw(graphics, time, alpha);

            if (!this.Visible)
            {
                return;
            }

            this.CreateRenderTarget();

            this.Children.BeginDraw(graphics, time, alpha);

            this.OnBeginDrawStarted(
                new RenderComponentDrawEventArgs(this.RenderTarget, this.RenderTargetDestinationRectangle, time));

            this.GraphicsDevice.SetRenderTarget(this.RenderTarget);
            this.GraphicsDevice.Clear(Color.Transparent);

            this.Draw(graphics, time, this.MixedAlpha(alpha));

            this.GraphicsDevice.SetRenderTarget(null);
        }

        /// <summary>
        /// Re-draw its own render target into parent's render target or back buffer.
        /// </summary>
        public sealed override void EndDraw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            base.EndDraw(graphics, time, alpha);

            if (!this.Visible)
            {
                return;
            }

            this.Children.EndDraw(graphics, time, alpha);

            this.OnEndDrawStarted(
                new RenderComponentDrawEventArgs(this.RenderTarget, this.RenderTargetDestinationRectangle, time));

            if (this.IsChild)
            {
                this.SetBackRenderTarget();
            }

            this.SpriteBatch.Begin();
            this.SpriteBatch.Draw(
                this.RenderTarget,
                this.RenderTargetDestinationRectangle,
                this.RenderTargetSourceRectangle,
                Color.White.MakeTransparent(this.MixedAlpha(alpha)));
            this.SpriteBatch.End();
        }

        #endregion

        #region Update

        public override void Update(GameTime time)
        {
            base.Update(time);

            if (!this.Enabled)
            {
                return;
            }

            this.Children.Update(time);
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            base.UpdateInput(input, time);

            if (!this.Enabled)
            {
                return;
            }

            this.Children.UpdateInput(input, time);
        }

        #endregion

        #region Buffer

        public override void UpdateForwardBuffer()
        {
            base.UpdateForwardBuffer();

            if (!this.Enabled)
            {
                return;
            }

            this.Children.UpdateForwardBuffer();
        }

        public override void UpdateBackwardBuffer()
        {
            base.UpdateBackwardBuffer();

            if (!this.Enabled)
            {
                return;
            }

            this.Children.UpdateBackwardBuffer();
        }

        #endregion
    }
}