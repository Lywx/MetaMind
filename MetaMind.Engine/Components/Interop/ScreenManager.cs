// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Screens.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components.Interop
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Screen;

    public class ScreenManager : GameInputableComponent, IScreenManager
    {
        #region Dependency

        /// <summary>
        /// A default SpriteBatch shared by all the screens. This saves
        /// each screen having to bother creating their own local instance.
        /// </summary>
        private SpriteBatch SpriteBatch { get; set; }

        public ScreenSettings Settings { get; set; }

        #endregion

        #region Graphics Data

        private Texture2D blankTexture;

        #endregion Graphics Data

        #region Screen Data

        private readonly List<IGameScreen> screens = new List<IGameScreen>();

        private readonly List<IGameScreen> screensToUpdate = new List<IGameScreen>();

        /// <summary>
        /// Expose an array holding all the screens. We return a copy rather
        /// than the real master list, because screens should only ever be added
        /// or removed using the AddScreen and RemoveScreen methods.
        /// </summary>
        public IGameScreen[] Screens => this.screens.ToArray();

        #endregion Screen Data

        #region State Data

        private bool isInitialized;

        #endregion State Data

        #region Trace Data

        private bool traceEnabled;

        /// <summary>
        /// If true, the manager prints out a list of all the screens
        /// each time it is updated. This can be useful for making sure
        /// everything is being added and removed at the right times.
        /// </summary>
        public bool TraceEnabled
        {
            get
            {
                return this.traceEnabled;
            }

            set
            {
                this.traceEnabled = value;
            }
        }

        #endregion Trace Data

        #region Constructors

        /// <summary>
        /// Constructs a new screen manager component.
        /// </summary>
        public ScreenManager(GameEngine engine, ScreenSettings settings, SpriteBatch spriteBatch)
            : base(engine)
        {
            if (engine == null)
            {
                throw new ArgumentNullException(nameof(engine));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            if (spriteBatch == null)
            {
                throw new ArgumentNullException(nameof(spriteBatch));
            }

            this.SpriteBatch = spriteBatch;
            this.Settings    = settings;
        }

        #endregion Constructors

        #region Initialization

        /// <summary>
        /// Initializes the screen manager component.
        /// </summary>
        public override void Initialize()
        {
            this.isInitialized = true;

            base.Initialize();            
        }

        #endregion Initialization

        #region Load and Unload

        protected override void LoadContent()
        {
            this.blankTexture = this.Interop.Content.Load<Texture2D>(@"Texture\Screens\Blank");

            // Tell each of the screens to load their content.
            foreach (var screen in this.screens)
            {
                screen.LoadContent(this.Interop);
            }
        }

        protected override void UnloadContent()
        {
            // Tell each of the screens to unload their content.
            foreach (var screen in this.screens)
            {
                screen.UnloadContent(this.Interop);
            }
        }

        #endregion Load and Unload

        #region Update and Draw

        /// <summary>
        /// Tells each screen to draw itself.
        /// </summary>
        public override void Draw(GameTime time)
        {
            if (
                // Don't draw when the game window is inactive to save CPU resources
                this.Game.IsActive ||

                // Used to provide a way to constantly draw
                this.Settings.IsAlwaysVisible)
            {
                this.ScreensVisible((screen, graphics, t) => screen.BeginDraw(graphics, t), this.Graphics, time);

                // Render target into back buffer
                this.ScreensVisible((screen, graphics, t) => screen.EndDraw  (graphics, t), this.Graphics, time);
            }
        }

        /// <summary>
        /// Allows each screen to run logic.
        /// </summary>
        public override void Update(GameTime time)
        {
            // Update the screen when users actually use it
            if (this.Game.IsActive || 
                this.Settings.IsAlwaysActive)
            {
                this.UpdateInternal(time);

                // Don't need an engine access here for plain Update(GameTime)
                this.ScreensAll<object>((screen, access, t) => screen.Update(t), null, time);
            }
        }

        public override void UpdateInput(GameTime time)
        {
            this.ScreensActive((screen, access, t) => screen.UpdateInput(access, t), this.Input, time);
        }

        private void UpdateInternal(GameTime time)
        {
            // Make a copy of the master screen list, to avoid confusion if
            // the process of updating one screen adds or removes others.
            this.screensToUpdate.Clear();
            foreach (var screen in this.screens)
            {
                this.screensToUpdate.Add(screen);
            }

            var hasOtherScreenFocus = !this.Game.IsActive;
            var isCoveredByOtherScreen = false;

            // Loop as long as there are screens waiting to be updated.
            while (this.screensToUpdate.Count > 0)
            {
                // Pop the topmost screen off the waiting list.
                var screen = this.screensToUpdate[this.screensToUpdate.Count - 1];

                this.screensToUpdate.RemoveAt(this.screensToUpdate.Count - 1);

                // Update the screen transition
                screen.UpdateScreen(this.Interop, time, hasOtherScreenFocus, isCoveredByOtherScreen);

                if (screen.ScreenState == GameScreenState.TransitionOn ||
                    screen.ScreenState == GameScreenState.Active)
                {
                    // If this is the first active screen we came across,
                    // give it a chance to handle input.
                    if (!hasOtherScreenFocus)
                    {
                        hasOtherScreenFocus = true;
                    }

                    // If this is an active non-popup, inform any subsequent
                    // screens that they are covered by it.
                    if (!screen.IsPopup)
                    {
                        isCoveredByOtherScreen = true;
                    }
                }
            }

            // Print debug trace?
            if (this.traceEnabled)
            {
                this.TraceScreens();
            }
        }

        #endregion Update and Draw

        #region Operations

        /// <summary>
        /// Adds a new screen to the screen manager.
        /// </summary>
        public void AddScreen(IGameScreen screen)
        {
            ((GameScreen)screen).IsExiting = false;

            // If we have a graphics device, tell the screen to load content.
            if (this.isInitialized)
            {
                screen.LoadContent(this.Interop);
            }

            this.screens.Add(screen);
        }

        /// <summary>
        /// Helper draws a translucent black fullscreen sprite, used for fading
        /// screens in and out, and for darkening the background behind popups.
        /// </summary>
        public void FadeScreen(float alpha, Color color)
        {
            var viewport = this.GraphicsDevice.Viewport;

            this.SpriteBatch.Begin();

            this.SpriteBatch.Draw(this.blankTexture, new Rectangle(0, 0, viewport.Width, viewport.Height), color * alpha);

            this.SpriteBatch.End();
        }

        /// <summary>
        /// Removes a screen from the screen manager. You should normally
        /// use Screen.Exit instead of calling this directly, so
        /// the screen can gradually transition off rather than just being
        /// instantly removed.
        /// </summary>
        public void RemoveScreen(IGameScreen screen)
        {
            // If we have a graphics device, tell the screen to unload content.
            if (this.isInitialized)
            {
                screen.UnloadContent(this.Interop);
            }

            this.screens.Remove(screen);
            this.screensToUpdate.Remove(screen);
        }

        public void EraseScreenFrom(int index)
        {
            this.screens.Skip(index).ToList().ForEach(s => s.Exit());
        }

        /// <summary>
        /// Prints a list of all the screens, for debugging.
        /// </summary>
        private void TraceScreens()
        {
            var names = new List<string>();

            foreach (var screen in this.screens.ToArray())
            {
                names.Add(screen.GetType().Name);
            }

            Debug.WriteLine(string.Join(", ", names.ToArray()));
        }

        #endregion Operations

        #region Helpers

        private void ScreensAll<TAccess>(
            Action<IGameScreen, TAccess, GameTime> action,
            TAccess access,
            GameTime time)
        {
            this.screens
                .ForEach(screen => action(screen, access, time));
        }

        private void ScreensActive<TAccess>(
            Action<IGameScreen, TAccess, GameTime> action,
            TAccess access,
            GameTime time)
        {
            this.screens
                .FindAll(screen => screen.IsActive)
                .ForEach(screen => action(screen, access, time));
        }

        private void ScreensVisible<TAccess>(
            Action<IGameScreen, TAccess, GameTime> action,
            TAccess access,
            GameTime time)
        {
            this.screens
                .FindAll(screen => screen.ScreenState != GameScreenState.Hidden)
                .ForEach(screen => action(screen, access, time));
        }

        #endregion

        #region Events

        /// <summary>
        /// Called when the Game Engine is closed by Exit / Restart operations.
        /// </summary>
        public void OnExiting()
        {
            this.UnloadContent();
        }

        #endregion

        #region IDisposable

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var screen in this.screens)
                {
                    screen.Dispose();
                }

                this.screens.Clear();

                this.blankTexture?.Dispose();
                this.blankTexture = null;

                // Note that sprite batch is not owned by this
                this.SpriteBatch = null;
            }

            base.Dispose(disposing);
        }

        #endregion
    }
}