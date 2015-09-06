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

        #region Engine Graphics

        protected internal readonly RenderTargetUsage TargetUsage = RenderTargetUsage.DiscardContents;

        public RenderTarget2D Target { get; protected set; }

        protected int ViewportHeight => this.Graphics.Settings.Height;

        protected int ViewportWidth => this.Graphics.Settings.Width;

        protected virtual int TargetWidth
        {
            get
            {
                var w = this.Width;

                if (w > this.ViewportWidth)
                    w = this.ViewportWidth;

                return w;
            }
        }

        protected virtual int TargetHeight
        {
            get
            {
                var h = this.Height;

                if (h > this.ViewportHeight)
                    h = this.ViewportHeight;

                return h;
            }
        }

        #endregion

        private bool invalidated;

        #region Events

        public event EventHandler DrawTarget;

        #endregion

        #region Event On

        private void OnBeginDraw(EventArgs e)
        {
            this.DrawTarget?.Invoke(this, e);
        }

        #endregion

        #region Initialization

        public bool Initialized { get; private set; }

        public virtual void Initialize()
        {
            this.Initialized = true;
        }

        #endregion

        #region Target

        private RenderTarget2D CreateRenderTarget(int width, int height)
        {
            if (width <= 0
                || height <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            return new RenderTarget2D(
                this.Graphics.GraphicsDevice,
                width,
                height,
                false,
                SurfaceFormat.Color,
                DepthFormat.None,
                this.Graphics.GraphicsDevice.PresentationParameters.MultiSampleCount,
                this.TargetUsage);
        }

        #endregion

        #region Draw

        /// <summary>
        /// Draws the entire scene in the given render target.
        /// </summary>
        public override void BeginDraw(IGameGraphicsService graphics, GameTime time)
        {
            base.BeginDraw(graphics, time);

            if (this.Visible)
            {
                if (this.invalidated)
                {
                    this.OnBeginDraw(new DrawEventArgs(new Rectangle(0, 0, this.TargetWidth, this.TargetHeight), time));

                    if (this.Target == null ||
                        this.Target.Width < this.Width ||
                        this.Target.Height < this.Height)
                    {
                        if (this.Target != null)
                        {
                            this.Target.Dispose();
                            this.Target = null;
                        }

                        this.Target = this.CreateRenderTarget(this.TargetWidth, this.TargetHeight);
                    }

                    if (this.Target != null)
                    {
                        graphics.GraphicsDevice.SetRenderTarget(this.Target);
                        graphics.GraphicsDevice.Clear(Color.Transparent);
                    }

                    this.invalidated = false;
                }
            }
        }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            base.Draw(graphics, time, alpha);
        }

        public override void EndDraw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            if (this.Visible && this.Target != null)
            {
                graphics.SpriteBatch.Begin();
                graphics.SpriteBatch.Draw(
                    this.Target,
                    this.Location.ToVector2(),
                    new Rectangle(0, 0, this.Width, this.Height),
                    Color.White.MakeTransparent(alpha));

                graphics.SpriteBatch.End();
            }
        }

        #endregion
    }
}