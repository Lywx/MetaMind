namespace MetaMind.Engine.Screens
{
    using System;
    using Guis.Controls;
    using Microsoft.Xna.Framework;
    using Services;

    public class GameLayer : Control, IGameLayer
    {
        #region Constructors and Finalizer

        public GameLayer(IGameScreen screen)
        {
            if (screen == null)
            {
                throw new ArgumentNullException(nameof(screen));
            }

            this.Screen = screen;

            this.Width  = this.Screen.Width;
            this.Height = this.Screen.Height;

            this.Location = new Point(0, 0);
        }

        ~GameLayer()
        {
            this.Dispose(true);
        }

        #endregion

        public IGameScreen Screen { get; private set; }

        private GameEngine Engine => this.Interop.Engine;

        #region Layer Events

        public event EventHandler FadedIn = delegate { };

        public event EventHandler FadedOut = delegate { };

        #endregion

        #region Layer States

        private bool isFading;

        #endregion

        #region Layer Graphics

        private readonly int transitionAlphaMax = 255;

        private readonly int transitionAlphaMin = 0;

        private float transitionAlpha = 255;

        public byte TransitionAlpha
        {
            get { return (byte)this.transitionAlpha; }
            set { this.transitionAlpha = value; }
        }

        #endregion

        #region Draw

        public Action<IGameGraphicsService, GameTime, byte> DrawAction { get; set; } = delegate { };

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            base.Draw(graphics, time, alpha);
            this.DrawAction?.Invoke(graphics, time, alpha);
        }

        #endregion

        #region Update

        public Action<GameTime> UpdateAction { get; set; } = delegate { };

        public Action<IGameInputService, GameTime> UpdateInputAction { get; set; } = delegate { };

        public override void Update(GameTime time)
        {
            base.Update(time);
            this.UpdateAction?.Invoke(time);
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
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
                if (this.TransitionAlpha < this.transitionAlphaMax)
                {
                    this.transitionAlpha += (float)this.transitionAlphaMax / fadeInFrameNum;

                    if (this.transitionAlpha > this.transitionAlphaMax - 0.1f)
                    {
                        this.transitionAlpha = this.transitionAlphaMax;
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
                if (this.TransitionAlpha > this.transitionAlphaMin)
                {
                    this.transitionAlpha -= (float)this.transitionAlphaMax / frames;

                    if (this.transitionAlpha < this.transitionAlphaMin + 0.1f)
                    {
                        this.transitionAlpha = this.transitionAlphaMin;
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
