namespace MetaMind.Engine.Guis.Controls
{
    using System;
    using Elements;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Services;

    public class Control : PickableFrame, IControl
    {
        protected Control()
        {
        }

        #region Render Data

        protected SpriteBatch SpriteBatch => this.Graphics.SpriteBatch;

        protected GraphicsDevice GraphicsDevice => this.Graphics.GraphicsDevice;

        protected int ViewportHeight => this.GraphicsDevice.Viewport.Height;

        protected int ViewportWidth => this.GraphicsDevice.Viewport.Width;

        protected RenderTarget2D RenderTarget { get; set; }

        protected virtual int RenderTargetWidth
        {
            get
            {
                var w = this.Width;

                if (w > this.ViewportWidth)
                    w = this.ViewportWidth;

                return w;
            }
        }

        protected virtual int RenderTargetHeight
        {
            get
            {
                var h = this.Height;

                if (h > this.ViewportHeight)
                    h = this.ViewportHeight;

                return h;
            }
        }

        private Rectangle RenderTargetRectangle => new Rectangle(0, 0, this.RenderTargetWidth, this.RenderTargetHeight);

        #endregion

        #region Events

        public event EventHandler DrawStarted = delegate { };

        #endregion

        #region Event On

        private void OnDrawStarted(EventArgs e)
        {
            this.DrawStarted?.Invoke(this, e);
        }

        #endregion

        private Control parent = null;

        private Control root = null;

        private ControlCollection Controls { get; set; } = new ControlCollection();

        /// <summary>
        /// Gets a value indicating whether this control is a child control.
        /// </summary>
        public virtual bool IsChild { get { return (parent != null); } }

        /// <summary>
        /// Gets a value indicating whether this control is a parent control.
        /// </summary>
        public virtual bool IsParent { get { return (this.Controls != null && this.Controls.Count > 0); } }

        /// <summary>
        /// Gets a value indicating whether this control is a root control.
        /// </summary>
        public virtual bool IsRoot { get { return (root == this); } }

        #region Initialization

        public bool Initialized { get; private set; }

        private bool PositionInitialized => this.Location.X > int.MinValue / 2 &&
                                            this.Location.Y > int.MinValue / 2;

        public virtual void Initialize()
        {
            if (!this.PositionInitialized)
            {
                throw new InvalidOperationException();
            }

            this.Initialized = true;
        }

        #endregion

        #region Target

        #endregion

        #region Draw

        /// <summary>
        /// Draws the entire scene in the given render target.
        /// </summary>
        public override void BeginDraw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            base.BeginDraw(graphics, time, alpha);

            if (this.Visible)
            {
                this.OnDrawStarted(
                    new DrawEventArgs(this.RenderTargetRectangle, time));

                if (this.RenderTarget == null ||
                    this.RenderTarget.Width != this.Width ||
                    this.RenderTarget.Height != this.Height)
                {
                    if (this.RenderTarget != null)
                    {
                        this.RenderTarget.Dispose();
                        this.RenderTarget = null;
                    }

                    this.RenderTarget = RenderTarget2DFactory.Create(
                        this.RenderTargetWidth,
                        this.RenderTargetHeight);
                }
            }

            this.GraphicsDevice.SetRenderTarget(this.RenderTarget);
            this.GraphicsDevice.Clear(Color.Transparent);

            this.Draw(graphics, time, alpha);

            this.GraphicsDevice.SetRenderTarget(null);
        }

        public override void EndDraw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            if (this.Visible)
            {
                this.SpriteBatch.Begin();
                this.SpriteBatch.Draw(
                    this.RenderTarget,
                    this.Location.ToVector2(),
                    this.RenderTargetRectangle,
                    Color.White.MakeTransparent(alpha));
                this.SpriteBatch.End();
            }
        }

        #endregion
    }
}