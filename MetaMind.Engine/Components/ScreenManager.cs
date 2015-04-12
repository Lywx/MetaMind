// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScreenManager.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    using MetaMind.Engine.Screens;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class ScreenManager : DrawableGameComponent
    {
        #region Singleton

        private static ScreenManager Singleton { get; set; }

        public static ScreenManager GetInstance(GameEngine gameEngine, ScreenSettings settings)
        {
            if (Singleton == null)
            {
                Singleton = new ScreenManager(gameEngine, settings);
            }

            if (gameEngine != null)
            {
                gameEngine.Components.Add(Singleton);
            }

            return Singleton;
        }

        #endregion Singleton

        #region Graphics Data

        private Texture2D blankTexture;

        private SpriteBatch spriteBatch;

        /// <summary>
        /// A default SpriteBatch shared by all the screens. This saves
        /// each screen having to bother creating their own local instance.
        /// </summary>
        public SpriteBatch SpriteBatch
        {
            get
            {
                return this.spriteBatch;
            }
        }

        #endregion

        #region Screen Data

        private readonly List<IGameScreen> screens = new List<IGameScreen>();

        private readonly List<IGameScreen> screensToUpdate = new List<IGameScreen>();

        /// <summary>
        /// Expose an array holding all the screens. We return a copy rather
        /// than the real master list, because screens should only ever be added
        /// or removed using the AddScreen and RemoveScreen methods.
        /// </summary>
        public IGameScreen[] Screens
        {
            get
            {
                return this.screens.ToArray();
            }
        }

        public ScreenSettings Settings { get; private set; }

        #endregion

        #region State Data

        private bool isInitialized;

        #endregion

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
                return traceEnabled;
            }

            set
            {
                traceEnabled = value;
            }
        }

        #endregion Trace

        #region Engine Data

        private IGameFile GameFile { get; set; }

        private IGameGraphics GameGraphics { get; set; }

        private IGameInput GameInput { get; set; }

        private IGameInterop GameInterop { get; set; }

        private IGameSound GameSound { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new screen manager component.
        /// </summary>
        private ScreenManager(GameEngine gameEngine, ScreenSettings settings)
            : base(gameEngine)
        {
            this.Settings = settings;

            this.GameFile     = new GameEngineFile(gameEngine);
            this.GameGraphics = new GameEngineGraphics(gameEngine);
            this.GameInput    = new GameEngineInput(gameEngine);
            this.GameInterop  = new GameEngineInterop(gameEngine);
            this.GameSound    = new GameEngineSound(gameEngine);
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Initializes the screen manager component.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            this.isInitialized = true;
        }

        #endregion

        #region Load and Unload

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);

            this.blankTexture = this.GameFile.Content.Load<Texture2D>(@"Textures\Screens\Blank");

            // Tell each of the screens to load their content.
            foreach (var screen in this.screens)
            {
                screen.Load(this.GameFile);
            }
        }

        protected override void UnloadContent()
        {
            // Tell each of the screens to unload their content.
            foreach (var screen in this.screens)
            {
                screen.Unload(this.GameFile);
            }
        }

        #endregion Constructors

        #region Update and Draw

        /// <summary>
        /// Tells each screen to draw itself.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            if (this.Game.IsActive || 
                this.Settings.IsAlwaysVisible)
            {
                foreach (var screen in this.screens.Where(screen => screen.ScreenState != GameScreenState.Hidden))
                {
                    screen.Draw(this.GameGraphics, gameTime);
                }
            }
        }

        /// <summary>
        /// Allows each screen to run logic.
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            // Make a copy of the master screen list, to avoid confusion if
            // the process of updating one screen adds or removes others.
            {
                this.screensToUpdate.Clear();

                foreach (var screen in this.screens)
                {
                    this.screensToUpdate.Add(screen);
                }
            }

            var hasOtherScreenFocus = !this.Game.IsActive;
            var isCoveredByOtherScreen = false;

            // Loop as long as there are screens waiting to be updated.
            while (this.screensToUpdate.Count > 0)
            {
                // Pop the topmost screen off the waiting list.
                var screen = screensToUpdate[screensToUpdate.Count - 1];

                this.screensToUpdate.RemoveAt(this.screensToUpdate.Count - 1);

                // Update the screen when users actually use it
                if (this.Game.IsActive || 
                    this.Settings.IsAlwaysActive)
                {
                    // Rudimentary screen update
                    screen.Update(this.GameGraphics, gameTime, hasOtherScreenFocus, isCoveredByOtherScreen);
                }

                if (screen.ScreenState == GameScreenState.TransitionOn || 
                    screen.ScreenState == GameScreenState.Active)
                {
                    // If this is the first active screen we came across,
                    // give it a chance to handle input.
                    if (!hasOtherScreenFocus)
                    {
                        screen.Update(this.GameFile   , gameTime);
                        screen.Update(this.GameInput  , gameTime);
                        screen.Update(this.GameInterop, gameTime);
                        screen.Update(this.GameSound  , gameTime);

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
            if (traceEnabled)
            {
                TraceScreens();
            }
        }

        #endregion Update and Draw

        #region Operations

        /// <summary>
        /// Adds a new screen to the screen manager.
        /// </summary>
        public void AddScreen(GameScreen screen)
        {
            screen.IsExiting = false;

            // If we have a graphics device, tell the screen to load content.
            if (this.isInitialized)
            {
                screen.Load(this.GameFile);
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

            this.spriteBatch.Begin();

            this.spriteBatch.Draw(this.blankTexture, new Rectangle(0, 0, viewport.Width, viewport.Height), color * alpha);

            this.spriteBatch.End();
        }

        /// <summary>
        /// Removes a screen from the screen manager. You should normally
        /// use Screen.Exit instead of calling this directly, so
        /// the screen can gradually transition off rather than just being
        /// instantly removed.
        /// </summary>
        public void RemoveScreen(GameScreen screen)
        {
            // If we have a graphics device, tell the screen to unload content.
            if (this.isInitialized)
            {
                screen.Unload(this.GameFile);
            }

            this.screens.Remove(screen);
            this.screensToUpdate.Remove(screen);
        }

        /// <summary>
        /// Prints a list of all the screens, for debugging.
        /// </summary>
        private void TraceScreens()
        {
            var names = new List<string>();

            foreach (var screen in this.screens)
            {
                names.Add(screen.GetType().Name);
            }

            Debug.WriteLine(string.Join(", ", names.ToArray()));
        }

        #endregion Operations
    }
}