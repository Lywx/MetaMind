namespace MetaMind.Engine.Gui.Control
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Service;

    public abstract class Control : ControlComponent, IControl
    {
        public Control()
        {
        }

        #region Geometry Data

        public virtual int Width { get; set; }

        public virtual int Height { get; set; }

        public virtual int X { get; set; }

        public virtual int Y { get; set; }

        public virtual Point Location
        {
            get { return new Point(this.X, this.Y); }
            set
            {
                this.X = value.X;
                this.Y = value.Y;
            }
        }

        #endregion

        #region State Data

        public bool Active { get; set; }

        #endregion

        #region Visual Data

        private byte alpha = byte.MaxValue;

        public virtual byte Alpha
        {
            get { return this.alpha; }
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

        public string Name { get; private set; } = "Control";

        public Control Parent { get; protected set; } = null;

        public Control Root { get; protected set; } = null;

        public ControlCollection Controls { get; protected set; } = new ControlCollection();

        /// <summary>
        /// Gets a value indicating whether this control is a child control.
        /// </summary>
        public virtual bool IsChild => (this.Parent != null);

        /// <summary>
        /// Gets a value indicating whether this control is a parent control.
        /// </summary>
        public virtual bool IsParent => (this.Controls != null && this.Controls.Count > 0);

        /// <summary>
        /// Gets a value indicating whether this control is a root control.
        /// </summary>
        public virtual bool IsRoot => (this.Root == this);

        public virtual void Add(Control control)
        {
            if (control != null)
            {
                if (!this.Controls.Contains(control))
                {
                    control.Parent?.Remove(control);

                    control.Parent = this;
                    control.Root = this.Root;
                    control.Enabled = (this.Enabled ? control.Enabled : this.Enabled);
                    this.Controls.Add(control);

                    this.Resize += control.OnParentResize;

                    if (this.Active)
                    {
                        this.OnParentChanged();
                    }
                }
            }
        }

        public virtual void Remove(Control control)
        {
            if (control != null)
            {
                this.Controls.Remove(control);

                control.Parent = null;
                control.Root = control;

                this.Resize -= control.OnParentResize;

                if (this.Active)
                {
                    this.OnParentChanged();
                }
            }
        }

        public virtual bool Contains(Control control, bool recursively)
        {
            if (this.Controls != null)
            {
                foreach (var c in this.Controls)
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

        protected SpriteBatch SpriteBatch => this.Graphics.SpriteBatch;

        protected GraphicsDevice GraphicsDevice => this.Graphics.GraphicsDevice;

        protected Viewport Viewport => this.GraphicsDevice.Viewport;

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

        protected Rectangle DestinationRectangle => new Rectangle(this.Location, this.RenderTargetSize);

        protected Rectangle SourceRectangle => new Rectangle(Point.Zero, this.RenderTargetSize);

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

        public event EventHandler Resize = delegate { };

        public event EventHandler<DrawEventArgs> BeginDrawStarted = delegate { };

        public event EventHandler<DrawEventArgs> EndDrawStarted = delegate { };

        public event EventHandler ParentChanged = delegate { };

        #endregion

        #region Event On

        private void OnAlphaChanged(EventArgs e)
        {
        }

        private void OnBeginDrawStarted(DrawEventArgs e)
        {
            this.BeginDrawStarted?.Invoke(this, e);
        }

        private void OnEndDrawStarted(DrawEventArgs e)
        {
            this.EndDrawStarted?.Invoke(this, e);
        }

        private void OnParentResize(object sender, EventArgs e)
        {
            
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

            foreach (var control in this.Controls)
            {
                control.BeginDraw(graphics, time, alpha);
            }

            this.OnBeginDrawStarted(
                new DrawEventArgs(this.RenderTarget, this.DestinationRectangle, time));

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

            foreach (var control in this.Controls)
            {
                control.EndDraw(graphics, time, alpha);
            }

            this.OnEndDrawStarted(
                new DrawEventArgs(this.RenderTarget, this.DestinationRectangle, time));

            if (this.IsChild)
            {
                this.SetBackRenderTarget();
            }

            this.SpriteBatch.Begin();
            this.SpriteBatch.Draw(
                this.RenderTarget,
                this.DestinationRectangle,
                this.SourceRectangle,
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

            foreach (var control in this.Controls)
            {
                control.Update(time);
            }
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            base.UpdateInput(input, time);

            if (!this.Enabled)
            {
                return;
            }

            foreach (var control in this.Controls)
            {
                control.UpdateInput(input, time);
            }
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

            foreach (var control in this.Controls)
            {
                control.UpdateForwardBuffer();
            }
        }

        public override void UpdateBackwardBuffer()
        {
            base.UpdateBackwardBuffer();

            if (!this.Enabled)
            {
                return;
            }

            foreach (var control in this.Controls)
            {
                control.UpdateBackwardBuffer();
            }
        }

        #endregion
    }
}