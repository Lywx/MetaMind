namespace MetaMind.Engine.Screen
{
    using System;
    using System.Diagnostics;
    using Components.Graphics.Adapters;
    using Entities;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Node;
    using Service;

    public class MMScreen : MMNode, IMMScreen
    {
        #region Geometry Data

        public int Width => this.Viewport.Width;

        public int Height => this.Viewport.Height;

        #endregion

        #region Render Data

        /// <summary>
        /// Target of the screen. Screen is the closest layer to back buffer.
        /// </summary>
        public RenderTarget2D RenderTarget { get; set; }

        protected Rectangle RenderTargetDestinationRectangle => this.Viewport.Bounds;

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

        private MMScreenState screenState = MMScreenState.TransitionOn;

        private float transitionPosition = 1;

        /// <summary>
        /// Checks whether this screen is active and can respond to user input.
        /// </summary>
        public bool IsActive => !this.HasOtherScreenFocus &&
                                (this.screenState == MMScreenState.TransitionOn ||
                                 this.screenState == MMScreenState.Active);

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
        public MMScreenState ScreenState
        {
            get { return this.screenState; }
            protected set { this.screenState = value; }
        }

        /// <summary>
        /// Gets the current alpha of the screen transition, ranging
        /// from 255 (fully active, no transition) to 0 (transitioned
        /// fully off to nothing).
        /// </summary>
        public byte TransitionOpacity => (byte)(255 - this.TransitionPosition * 255);

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

        #region Layer Data

        protected MMEntityCollection<IMMLayer> Layers { get; private set; } = new MMEntityCollection<IMMLayer>();

        #endregion

        #region Constructors and Finalizer

        public MMScreen()
        {
            this.ViewportAdapter = new ScalingViewportAdapter(this.GraphicsDevice, this.Width, this.Height);
        }

        ~MMScreen()
        {
            this.Dispose();
        }

        #endregion

        #region Load and Unload

        public virtual void LoadContent(IMMEngineInteropService interop)
        {
            this.Layers.LoadContent(interop);
        }

        public virtual void UnloadContent(IMMEngineInteropService interop)
        {
            this.Layers.UnloadContent(interop);
        }
        
        #endregion

        #region Draw

        /// <summary>
        /// Start drawing in screen.
        /// </summary>
        public void BeginDraw(IMMEngineGraphicsService graphics, GameTime time)
        {
            this.CreateRenderTarget();

            this.Draw(graphics, time);
        }

        /// <summary>
        /// Draw subroutine.
        /// </summary>
        protected void Draw(IMMEngineGraphicsService graphics, GameTime time)
        {
            this.DrawToComponents(graphics, time);
            this.DrawToScreen(graphics, time);
        }

        /// <summary>
        /// Draw to each individual render component's render target.
        /// </summary>
        private void DrawToComponents(IMMEngineGraphicsService graphics, GameTime time)
        {
            if (this.Layers.Count != 0)
            {
                foreach (
                    var layer in
                        this.Layers.FindAll(
                            layer => layer.Active && layer.Visible))
                {
                    layer.BeginDraw(graphics, time, this.TransitionOpacity);
                }
            }
        }

        /// <summary>
        /// Draw children layers to screen's render target.
        /// </summary>
        private void DrawToScreen(IMMEngineGraphicsService graphics, GameTime time)
        {
#if DEBUG
            Debug.Assert(this.RenderTarget != null);
#endif
            this.GraphicsDevice.SetRenderTarget(this.RenderTarget);
            this.GraphicsDevice.Clear(Color.Transparent);

            if (this.Layers.Count != 0)
            {
                foreach (
                    var layer in
                        this.Layers.FindAll(
                            layer => layer.Active && layer.Visible))
                {
                    layer.EndDraw(graphics, time, this.TransitionOpacity);
                }
            }

            this.GraphicsDevice.SetRenderTarget(null);
        }

        public virtual void EndDraw(IMMEngineGraphicsService graphics, GameTime time)
        {
            this.GraphicsDevice.SetRenderTarget(null);

            this.SpriteBatch.Begin();
            this.SpriteBatch.Draw(
                this.RenderTarget,
                this.RenderTargetDestinationRectangle,
                Color.White);
            this.SpriteBatch.End();
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

            // MMLayer is subclass of MMEntity. It may cache actions, they are updated there.
            foreach (var layer in this.Layers.FindAll(layer => layer.Active))
            {
                layer.Update(time);
            }
        }

        public virtual void UpdateScreen(IMMEngineInteropService interop, GameTime time, bool hasOtherScreenFocus, bool isCoveredByOtherScreen)
        {
            this.HasOtherScreenFocus = hasOtherScreenFocus;

            if (this.IsExiting)
            {
                // If the screen is going away to die, it should transition off.
                this.screenState = MMScreenState.TransitionOff;

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
                    this.screenState = MMScreenState.TransitionOff;
                }
                else
                {
                    // Transition finished!
                    this.screenState = MMScreenState.Hidden;
                }
            }
            else
            {
                // Otherwise the screen should transition on and become active.
                if (this.UpdateTransition(time, this.transitionOnTime, -1))
                {
                    // Still busy transitioning.
                    this.screenState = MMScreenState.TransitionOn;
                }
                else
                {
                    // Transition finished!
                    this.screenState = MMScreenState.Active;
                }
            }
        }

        public void UpdateInput(IMMEngineInputService input, GameTime time)
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

        #region Render Operations

        private void CreateRenderTarget()
        {
            if (this.RenderTarget == null)
            {
                this.RenderTarget = MMRenderTargetFactory.Create(
                    this.Width,
                    this.Height);
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