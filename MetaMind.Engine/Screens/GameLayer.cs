namespace MetaMind.Engine.Screens
{
    using System;
    using Microsoft.Xna.Framework;

    public class GameLayer : GameControllableEntity, IGameLayer
    {
        public GameLayer(IGameScreen screen, byte transitionAlpha = byte.MaxValue)
        {
            if (screen == null)
            {
                throw new ArgumentNullException(nameof(screen));
            }

            this.Screen = screen;

            this.transitionAlpha = transitionAlpha;
        }

        public IGameScreen Screen { get; private set; }

        private GameEngine Engine => this.GameInterop.Engine;

        #region Layer States

        private bool isFading;

        public bool IsActive { get; set; } = true;

        #endregion

        #region Layer Graphics

        private readonly int transitionAlphaMax = 255;

        private readonly int transitionAlphaMin = 0;

        private float transitionAlpha;

        public byte TransitionAlpha => (byte)this.transitionAlpha;

        #endregion

        #region Update

        public void UpdateTransition(GameTime time)
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

            var frames = this.TimeToFrame(time);

            Action increase = null;
            increase = () =>
            {
                if (this.TransitionAlpha < this.transitionAlphaMax)
                {
                    this.transitionAlpha += (float)this.transitionAlphaMax / frames;

                    if (this.transitionAlpha > this.transitionAlphaMax - 0.1f)
                    {
                        this.transitionAlpha = this.transitionAlphaMax;
                    }

                    this.DeferAction(increase);
                }
                else
                {
                    this.IsActive = true;
                    this.isFading = false;
                }
            };

            this.DeferAction(increase);
            this.isFading = true;
        }

        public void FadeOut(TimeSpan time)
        {
            if (this.isFading)
            {
                this.DeferAction(() => this.FadeOut(time));

                return;
            }

            var frames = this.TimeToFrame(time);

            Action decrease = null;
            decrease = () =>
            {
                if (this.TransitionAlpha > this.transitionAlphaMin)
                {
                    this.transitionAlpha -= (float)this.transitionAlphaMax / frames;

                    if (this.transitionAlpha < this.transitionAlphaMin + 0.1f)
                    {
                        this.transitionAlpha = this.transitionAlphaMin;
                    }

                    this.DeferAction(decrease);
                }
                else
                {
                    this.IsActive = false;
                    this.isFading = false;
                }
            };

            this.DeferAction(decrease);
            this.isFading = true;
        }

        #endregion

        private int TimeToFrame(TimeSpan time)
        {
            return (int)(time.TotalMilliseconds / this.Engine.TargetElapsedTime.Milliseconds) + 1;
        }
    }
}
