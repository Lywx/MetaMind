namespace MetaMind.Engine.Guis.Controls
{
    using Elements;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Control : DraggableFrame, IControl
    {
        protected Control(Container container)
        {
            this.Container = container;
        }

        #region Engine Graphics Data

        protected internal readonly RenderTargetUsage RenderTargetUsage = RenderTargetUsage.DiscardContents;

        protected int ViewportHeight => this.Graphics.Settings.Height;

        protected int ViewportWidth => this.Graphics.Settings.Width;

        #endregion

        public Container Container { get; }

        #region Initialization

        public bool Initialized { get; private set; }

        public virtual void Initialize()
        {
            this.Initialized = true;
        }

        #endregion

        #region Render Target

        public virtual RenderTarget2D CreateRenderTarget()
        {
            return CreateRenderTarget(this.ViewportWidth, this.ViewportHeight);
        }

        public virtual RenderTarget2D CreateRenderTarget(int width, int height)
        {
            return new RenderTarget2D(
                this.Module.GraphicsDevice,
                width,
                height,
                false,
                SurfaceFormat.Color,
                DepthFormat.None,
                this.Module.GraphicsDevice.PresentationParameters.MultiSampleCount,
                this.RenderTargetUsage);
        }

        private RenderTarget2D target;

        internal virtual void PrepareTexture(GameTime time)
        {
            if (this.Visible)
            {
                if (invalidated)
                {
                    OnDrawTexture(new DrawEventArgs(renderer, new Rectangle(0, 0, OriginWidth, OriginHeight), time));

                    if (target == null || target.Width < OriginWidth || target.Height < OriginHeight)
                    {
                        if (target != null)
                        {
                            target.Dispose();
                            target = null;
                        }

                        int w = OriginWidth + (Manager.TextureResizeIncrement - (OriginWidth % Manager.TextureResizeIncrement));
                        int h = OriginHeight + (Manager.TextureResizeIncrement - (OriginHeight % Manager.TextureResizeIncrement));

                        if (h > Manager.TargetHeight) h = Manager.TargetHeight;
                        if (w > Manager.TargetWidth) w = Manager.TargetWidth;

                        target = this.CreateRenderTarget(w, h);
                    }

                    if (target != null)
                    {
                        this.Module.GraphicsDevice.SetRenderTarget(target);
                        target.GraphicsDevice.Clear(backColor);

                        Rectangle rect = new Rectangle(0, 0, OriginWidth, OriginHeight);
                        DrawControls(renderer, rect, time, false);

                        this.Module.GraphicsDevice.SetRenderTarget(null);
                    }

                    invalidated = false;
                }
            }
        }

        #endregion
    }
}