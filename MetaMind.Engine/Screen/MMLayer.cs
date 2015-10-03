namespace MetaMind.Engine.Screen
{
    using System;
    using Gui.Renders;
    using Microsoft.Xna.Framework;
    using Service;

    public class MMLayer : MMRenderComponent, IMMLayer
    {
        #region Constructors and Finalizer

        public MMLayer(IMMScreen screen)
        {
            if (screen == null)
            {
                throw new ArgumentNullException(nameof(screen));
            }

            this.Screen = screen;

            this.Location = Point.Zero;
            this.Width    = this.Screen.Width;
            this.Height   = this.Screen.Height;
        }

        ~MMLayer()
        {
            this.Dispose(true);
        }

        #endregion

        #region Render Data

        public IMMScreen Screen { get; private set; }

        protected override void SetBackRenderTarget()
        {
            this.GraphicsDevice.SetRenderTarget(this.Screen.RenderTarget);
        }

        #endregion

        #region Control Data

        public override sealed Point Location
        {
            get { return base.Location; }
            set { base.Location = value; }
        }

        public override sealed int Width
        {
            get { return base.Width; }
            set { base.Width = value; }
        }

        public override sealed int Height
        {
            get { return base.Height; }
            set { base.Height = value; }
        }

        #endregion

        #region Layer Events

        public event EventHandler FadedIn = delegate { };

        public event EventHandler FadedOut = delegate { };

        #endregion

        #region Layer States

        private bool isFading;

        #endregion

        #region Layer Graphics

        private readonly int alphaMax = 255;

        private readonly int alphaMin = 0;

        private float alpha = 255;

        public override byte Opacity
        {
            get { return (byte)this.alpha; }
            set { this.alpha = value; }
        }

        #endregion

        #region Draw

        public Action<IMMEngineGraphicsService, GameTime, byte> DrawAction { get; set; } = delegate { };

        public override void Draw(IMMEngineGraphicsService graphics, GameTime time, byte alpha)
        {
            alpha = this.MixedMinOpacity(alpha);

            base.Draw              (graphics, time, alpha);
            this.DrawAction?.Invoke(graphics, time, alpha);
        }

        #endregion

        #region Update

        public Action<GameTime> UpdateAction { get; set; } = delegate { };

        public Action<IMMEngineInputService, GameTime> UpdateInputAction { get; set; } = delegate { };

        public override void Update(GameTime time)
        {
            base.Update(time);
            this.UpdateAction?.Invoke(time);
        }

        public override void UpdateInput(IMMEngineInputService input, GameTime time)
        {
            base.UpdateInput(input, time);
            this.UpdateInputAction?.Invoke(input, time);
        }

        public virtual void UpdateTransition(GameTime time)
        {
            this.ContinueAction(time);
        }

        #endregion

        #region Operations 

        public void FadeIn(TimeSpan time)
        {
            if (this.isFading)
            {
                this.DeferAction(() => this.FadeIn(time));

                return;
            }

            this.DeferAction(this.FadeInNextFrame(this.TimeToFrame(time)));
        }

        private Action FadeInNextFrame(int fadeInFrameNum)
        {
            return () =>
            {
                if (this.Opacity < this.alphaMax)
                {
                    this.alpha += (float)this.alphaMax / fadeInFrameNum;

                    if (this.alpha > this.alphaMax - 0.1f)
                    {
                        this.alpha = this.alphaMax;
                    }

                    this.DeferAction(this.FadeInNextFrame(fadeInFrameNum));

                    this.isFading = true;
                }
                else
                {
                    this.FadedIn(this, EventArgs.Empty);

                    this.Active = true;
                    this.isFading = false;
                }
            };
        }

        public void FadeOut(TimeSpan time)
        {
            if (this.isFading)
            {
                this.DeferAction(() => this.FadeOut(time));

                return;
            }

            this.DeferAction(this.FadeOutNextFrame(this.TimeToFrame(time)));
        }

        private Action FadeOutNextFrame(int frames)
        {
            return () =>
            {
                if (this.Opacity > this.alphaMin)
                {
                    this.alpha -= (float)this.alphaMax / frames;

                    if (this.alpha < this.alphaMin + 0.1f)
                    {
                        this.alpha = this.alphaMin;
                    }

                    this.DeferAction(this.FadeOutNextFrame(frames));

                    this.isFading = true;
                }
                else
                {
                    this.FadedOut(this, EventArgs.Empty);

                    this.Active = false;
                    this.isFading = false;
                }
            };
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
                        this.FadedOut = null;
                        this.FadedIn  = null;
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

        #region Time Helpers

        private int TimeToFrame(TimeSpan time) => (int)(time.TotalMilliseconds / this.Engine.TargetElapsedTime.Milliseconds) + 1;

        #endregion
    }
}
