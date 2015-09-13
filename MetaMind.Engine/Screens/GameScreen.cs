namespace MetaMind.Engine.Screens
{
    using System;
    using System.Diagnostics;
    using Services;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class GameScreen : IGameScreen
    {
        #region Render Data

        public int Width => this.Viewport.Width;

        public int Height => this.Viewport.Height;

        protected GraphicsDevice GraphicsDevice => this.Graphics.GraphicsDevice;

        protected SpriteBatch SpriteBatch => this.Graphics.SpriteBatch;

        /// <summary>
        /// Target of the screen. Screen is the closest layer to back buffer.
        /// </summary>
        protected virtual RenderTarget2D RenderTarget { get; set; }

        protected Rectangle RenderTargetRectangle => this.Viewport.Bounds;

        private Viewport Viewport => this.GraphicsDevice.Viewport;

        #endregion

        #region Screen Data

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

        #endregion Screen Data

        #region Screen State 

        private bool isExiting = false;

        private GameScreenState screenState = GameScreenState.TransitionOn;

        private float transitionPosition = 1;

        /// <summary>
        /// Checks whether this screen is active and can respond to user input.
        /// </summary>
        public bool IsActive => !this.HasOtherScreenFocus &&
                                (this.screenState == GameScreenState.TransitionOn ||
                                 this.screenState == GameScreenState.Active);

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
        public byte TransitionAlpha => (byte)(255 - this.TransitionPosition * 255);

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

        #endregion Screen State

        #region Screen Events

        public event EventHandler Exiting;

        public void OnExiting()
        {
            this.Exiting?.Invoke(this, EventArgs.Empty);
        }

        #endregion Screen Events

        #region Layers

        protected GameControllableEntityCollection<IGameLayer> Layers { get; private set; }

        private byte LayerTransitionAlpha(IGameLayer layer)
        {
            return Math.Min(layer.TransitionAlpha, this.TransitionAlpha);
        }

        #endregion

        #region Engine Data

        protected IGameGraphicsService Graphics => GameEngine.Service.Graphics;

        protected IGameInteropService Interop => GameEngine.Service.Interop;

        protected IGameNumericalService Numerical => GameEngine.Service.Numerical;

        #endregion

        #region Constructors and Finalizer

        public GameScreen()
        {
            this.Layers = new GameControllableEntityCollection<IGameLayer>();
        }

        ~GameScreen()
        {
            this.Dispose();
        }

        #endregion

        #region Load and Unload

        public virtual void LoadContent(IGameInteropService interop)
        {
            this.Layers.LoadContent(interop);
        }

        public virtual void UnloadContent(IGameInteropService interop)
        {
            this.Layers.UnloadContent(interop);
        }

        #endregion

        #region Draw

        public void BeginDraw(IGameGraphicsService graphics, GameTime time)
        {
            if (this.RenderTarget == null)
            {
                this.RenderTarget = RenderTarget2DFactory.Create(
                    this.Width,
                    this.Height);

            }

            this.Draw(graphics, time);
            this.DrawToScreen(graphics, time);
        }

        protected void Draw(IGameGraphicsService graphics, GameTime time)
        {
            if (this.Layers.Count != 0)
            {
                foreach (
                    var layer in
                        this.Layers.FindAll(
                            layer => layer.Active && layer.Visible))
                {
                    layer.BeginDraw(graphics, time, this.LayerTransitionAlpha(layer));
                }
            }
        }

        public virtual void EndDraw(IGameGraphicsService graphics, GameTime time)
        {
            graphics.SpriteBatch.Begin();
            graphics.SpriteBatch.Draw(
                this.RenderTarget,
                this.RenderTargetRectangle,
                Color.White);
            graphics.SpriteBatch.End();
        }

        private void DrawToScreen(IGameGraphicsService graphics, GameTime time)
        {
#if DEBUG
            Debug.Assert(this.RenderTarget != null);
#endif
            this.GraphicsDevice.SetRenderTarget(this.RenderTarget);
            this.GraphicsDevice.Clear(Color.Transparent);

            if (this.Layers.Count != 0)
            {
                // Draw layers to screen's render target
                foreach (
                    var layer in
                        this.Layers.FindAll(
                            layer => layer.Active && layer.Visible))
                {
                    layer.EndDraw(
                        graphics,
                        time,
                        this.LayerTransitionAlpha(layer));
                }
            }

            this.GraphicsDevice.SetRenderTarget(null);
        }

        #endregion Draw

#region Update

        public void Update(GameTime time)
        {
            // Layer transition inside screen.
            foreach (var layer in this.Layers.FindAll(layer => layer.Active))
            {
                layer.UpdateTransition(time);
            }

            // GameLayer is subclass of GameEntity. It may cache actions, they are updated there.
            foreach (var layer in this.Layers.FindAll(layer => layer.Active))
            {
                layer.Update(time);
            }
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

        public void UpdateInput(IGameInputService input, GameTime time)
        {
            this.Layers
                .FindAll(layer => layer.Active)
                .ForEach(layer => layer.UpdateInput(input, time));
        }

        /// <summary>
        /// Helper for updating the screen transition position.
        /// </summary>
        private bool UpdateTransition(GameTime time, TimeSpan transitionOff, int direction)
        {
            // How much should we move by?
            float transitionDelta;

            if (transitionOff == TimeSpan.Zero)
            {
                transitionDelta = 1;
            }
            else
            {
                transitionDelta = (float)(time.ElapsedGameTime.TotalMilliseconds / transitionOff.TotalMilliseconds);
            }

            // Update the transition position.
            this.transitionPosition += transitionDelta * direction;

            // Did we reach the end of the transition?
            if (this.transitionPosition <= 0 || 
                this.transitionPosition >= 1)
            {
                this.transitionPosition = MathHelper.Clamp(this.transitionPosition, 0, 1);
                return false;
            }

            // Otherwise we are still busy transitioning.
            return true;
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

#region IDisposable

        public void Dispose()
        {
            if (this.Layers != null)
            {
                this.RenderTarget.Dispose();

                this.DisposeLayers();
            }
        }

        private void DisposeLayers()
        {
            foreach (var layer in this.Layers)
            {
                layer.Dispose();
            }

            this.Layers.Clear();
            this.Layers = null;
        }

#endregion IDisposable
    }
}