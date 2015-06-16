namespace MetaMind.Engine.Screens
{
    using System;

    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class GameScreen : IGameScreen
    {
        #region Screen Options Data

        private bool isPopup = false;

        private TimeSpan transitionOffTime = TimeSpan.Zero;

        private TimeSpan transitionOnTime = TimeSpan.Zero;

        /// <summary>
        /// This property indicates whether the screen is only a small
        /// popup, in which case screens underneath it do not need to bother
        /// transitioning off.
        /// </summary>
        public bool IsPopup
        {
            get { return this.isPopup; }
            protected set { this.isPopup = value; }
        }

        /// <summary>
        /// Indicates how long the screen takes to
        /// transition off when it is deactivated.
        /// </summary>
        public TimeSpan TransitionOffTime
        {
            get { return this.transitionOffTime; }
            protected set { this.transitionOffTime = value; }
        }

        /// <summary>
        /// Indicates how long the screen takes to
        /// transition on when it is activated.
        /// </summary>
        public TimeSpan TransitionOnTime
        {
            get { return this.transitionOnTime; }
            protected set { this.transitionOnTime = value; }
        }

        #endregion Screen Options Data

        #region Screen State Data

        private bool isExiting = false;

        private GameScreenState screenState = GameScreenState.TransitionOn;

        private float transitionPosition = 1;

        /// <summary>
        /// Checks whether this screen is active and can respond to user input.
        /// </summary>
        public bool IsActive
        {
            get
            {
                return !this.HasOtherScreenFocus &&
                       (this.screenState == GameScreenState.TransitionOn ||
                        this.screenState == GameScreenState.Active);
            }
        }

        /// <summary>
        /// There are two possible reasons why a screen might be transitioning
        /// off. It could be temporarily going away to make room for another
        /// screen that is on top of it, or it could be going away for good.
        /// This property indicates whether the screen is exiting for real:
        /// if set, the screen will automatically remove itself as soon as the
        /// transition finishes.
        /// </summary>
        public bool IsExiting
        {
            get { return this.isExiting; }
            protected internal set
            {
                // fire when set false to true
                var exiting = !this.isExiting && value;

                this.isExiting = value;

                if (exiting)
                {
                    this.OnExiting();
                }
            }
        }

        /// <summary>
        /// Gets the current screen transition state.
        /// </summary>
        public GameScreenState ScreenState
        {
            get { return this.screenState; }
            protected set { this.screenState = value; }
        }

        /// <summary>
        /// Gets the current alpha of the screen transition, ranging
        /// from 255 (fully active, no transition) to 0 (transitioned
        /// fully off to nothing).
        /// </summary>
        public byte TransitionAlpha
        {
            get { return (byte)(255 - this.TransitionPosition * 255); }
        }

        /// <summary>
        /// Gets the current position of the screen transition, ranging
        /// from zero (fully active, no transition) to one (transitioned
        /// fully off to nothing).
        /// </summary>
        public float TransitionPosition
        {
            get { return this.transitionPosition; }
            protected set { this.transitionPosition = value; }
        }

        protected bool HasOtherScreenFocus { get; set; }

        #endregion Screen State Data

        #region Screen Events

        public event EventHandler Exiting;

        public void OnExiting()
        {
            if (this.Exiting != null)
            {
                this.Exiting(this, EventArgs.Empty);
            }
        }

        #endregion Screen Events

        #region Load and Unload

        public virtual void LoadContent(IGameInteropService interop)
        {
        }


        public virtual void UnloadContent(IGameInteropService interop)
        {
        }

        #endregion Load and Unload

        #region Draw

        /// <summary>
        /// This is called when the screen should draw itself.
        /// </summary>
        public virtual void Draw(IGameGraphicsService graphics, GameTime time)
        {
        }

        #endregion Draw

        #region Update

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void UpdateInput(IGameInputService input, GameTime time)
        {
        }

        public virtual void UpdateScreen(IGameInteropService interop, GameTime time, bool hasOtherScreenFocus, bool isCoveredByOtherScreen)
        {
            this.HasOtherScreenFocus = hasOtherScreenFocus;

            if (this.IsExiting)
            {
                // If the screen is going away to die, it should transition off.
                this.screenState = GameScreenState.TransitionOff;

                if (!this.UpdateTransition(time, this.transitionOffTime, 1))
                {
                    // When the transition finishes, remove the screen.
                    interop.Screen.RemoveScreen(this);
                }
            }
            else if (isCoveredByOtherScreen)
            {
                // If the screen is covered by another, it should transition off.
                if (this.UpdateTransition(time, this.transitionOffTime, 1))
                {
                    // Still busy transitioning.
                    this.screenState = GameScreenState.TransitionOff;
                }
                else
                {
                    // Transition finished!
                    this.screenState = GameScreenState.Hidden;
                }
            }
            else
            {
                // Otherwise the screen should transition on and become active.
                if (this.UpdateTransition(time, this.transitionOnTime, -1))
                {
                    // Still busy transitioning.
                    this.screenState = GameScreenState.TransitionOn;
                }
                else
                {
                    // Transition finished!
                    this.screenState = GameScreenState.Active;
                }
            }
        }

        /// <summary>
        /// Helper for updating the screen transition position.
        /// </summary>
        private bool UpdateTransition(GameTime gameTime, TimeSpan transitionOff, int direction)
        {
            // How much should we move by?
            float transitionDelta;

            if (transitionOff == TimeSpan.Zero)
            {
                transitionDelta = 1;
            }
            else
            {
                transitionDelta = (float)(gameTime.ElapsedGameTime.TotalMilliseconds / transitionOff.TotalMilliseconds);
            }

            // Update the transition position.
            this.transitionPosition += transitionDelta * direction;

            // Did we reach the end of the transition?
            if ((this.transitionPosition <= 0) || (this.transitionPosition >= 1))
            {
                this.transitionPosition = MathHelper.Clamp(this.transitionPosition, 0, 1);
                return false;
            }

            // Otherwise we are still busy transitioning.
            return true;
        }

        #endregion Update

        #region IDisposable

        public virtual void Dispose()
        {
        }

        #endregion IDisposable

        protected GameScreen()
        {
            this.SetupService();
        }

        #region Service

        protected IGameInteropService Interop { get; set; }

        private void SetupService()
        {
            if (GameEngine.Service != null)
            {
                this.Interop = GameEngine.Service.Interop;
            }
        }

        #endregion

        #region Operations

        /// <summary>
        /// Tells the screen to go away. Unlike Screens.RemoveScreen, which
        /// instantly kills the screen, this method respects the transition timings
        /// and will give the screen a chance to gradually transition off.
        /// </summary>
        public void Exit()
        {
            // Flag that it should transition off and then exit.
            this.IsExiting = true;

            // If the screen has a zero transition time, remove it immediately.
            // This clause is an extreme case which usually won't be triggered.
            if (this.TransitionOffTime == TimeSpan.Zero)
            {
                var screen = this.Interop.Screen;
                screen.RemoveScreen(this);
            }
        }

        #endregion Operations
    }
}