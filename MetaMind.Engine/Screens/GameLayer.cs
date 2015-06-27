namespace MetaMind.Engine.Screens
{
    using System;
    using Microsoft.Xna.Framework;

    public class GameLayer : GameControllableEntity, IGameLayer
    {
        private bool isFading;

        public GameLayer(IGameScreen screen, byte transitionAlpha = byte.MaxValue)
        {
            if (screen == null)
            {
                throw new ArgumentNullException("screen");
            }

            this.Screen = screen;

            this.transitionAlpha = transitionAlpha;

            this.IsActive = true;
        }

        public IGameScreen Screen { get; private set; }

        private GameEngine Engine
        {
            get { return this.GameInterop.Engine; }
        }

        #region Layer States

        public bool IsActive { get; set; }

        #endregion

        #region Layer Graphics

        private float transitionAlpha;

        public byte TransitionAlpha
        {
            get { return (byte)this.transitionAlpha; }
        }

        #endregion

        #region Operations 

        public void FadeIn(TimeSpan time)
        {
            if (this.isFading)
            {
                throw new InvalidOperationException("Layer is already fading.");
            }

            var transitionCount = this.TransitionCount(time);

            Action increase = null;
            increase = () =>
            {
                if (this.TransitionAlpha < 255)
                {
                    this.transitionAlpha += (float)255 / transitionCount;

                    if (this.transitionAlpha > 255 - 0.1f)
                    {
                        this.transitionAlpha = 255;
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
                throw new InvalidOperationException("Layer is already fading.");
            }

            var transitionCount = this.TransitionCount(time);

            Action decrease = null;
            decrease = () =>
            {
                if (this.TransitionAlpha > 0)
                {
                    this.transitionAlpha -= (float)255 / transitionCount;

                    if (this.transitionAlpha < 0 + 0.1f)
                    {
                        this.transitionAlpha = 0;
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

        private int TransitionCount(TimeSpan time)
        {
            return (int)(time.TotalMilliseconds / this.Engine.TargetElapsedTime.Milliseconds) + 1;
        }

        #endregion

        public void UpdateTransition(GameTime time)
        {
            this.ContinueAction(time);
        }
    }
}
