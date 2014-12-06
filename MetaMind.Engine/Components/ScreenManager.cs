// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScreenManager.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
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

        private static ScreenManager singleton;

        public static ScreenManager GetInstance(Game game)
        {
            if (singleton == null)
            {
                singleton = new ScreenManager(game);
            }

            if (game != null)
            {
                game.Components.Add(singleton);
            }

            return singleton;
        }

        #endregion Singleton

        #region Screens

        private readonly List<GameScreen> screens = new List<GameScreen>();

        private readonly List<GameScreen> screensToUpdate = new List<GameScreen>();

        private Texture2D blankTexture;

        private bool isInitialized;

        private SpriteBatch spriteBatch;

        #endregion Screens

        #region Trace

        private bool traceEnabled;

        #endregion Trace

        #region Constructors

        /// <summary>
        /// Constructs a new screen manager component.
        /// </summary>
        private ScreenManager(Game game)
            : base(game)
        {
        }

        /// <summary>
        /// Initializes the screen manager component.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            isInitialized = true;
        }

        /// <summary>
        /// Load your graphics content.
        /// </summary>
        protected override void LoadContent()
        {
            // Load content belonging to the screen manager.
            this.blankTexture = this.Game.Content.Load<Texture2D>(@"Textures\Screens\Blank");
            
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Tell each of the screens to load their content.
            foreach (var screen in screens)
            {
                screen.LoadContent();
            }
        }

        /// <summary>
        /// Unload your graphics content.
        /// </summary>
        protected override void UnloadContent()
        {
            // Tell each of the screens to unload their content.
            foreach (GameScreen screen in screens)
            {
                screen.UnloadContent();
            }
        }

        #endregion Constructors

        #region Update and Draw

        /// <summary>
        /// Tells each screen to draw itself.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            foreach (GameScreen screen in screens.Where(screen => screen.ScreenState != ScreenState.Hidden))
            {
                screen.Draw(gameTime);
            }
        }

        /// <summary>
        /// Allows each screen to run logic.
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            // Make a copy of the master screen list, to avoid confusion if
            // the process of updating one screen adds or removes others.
            screensToUpdate.Clear();

            foreach (GameScreen screen in screens)
            {
                screensToUpdate.Add(screen);
            }

            bool otherScreenHasFocus = !Game.IsActive;
            bool coveredByOtherScreen = false;

            // Loop as long as there are screens waiting to be updated.
            while (screensToUpdate.Count > 0)
            {
                // Pop the topmost screen off the waiting list.
                GameScreen screen = screensToUpdate[screensToUpdate.Count - 1];

                screensToUpdate.RemoveAt(screensToUpdate.Count - 1);

                // Update the screen.
                screen.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

                if (screen.ScreenState == ScreenState.TransitionOn || screen.ScreenState == ScreenState.Active)
                {
                    // If this is the first active screen we came across,
                    // give it a chance to handle input.
                    if (!otherScreenHasFocus)
                    {
                        screen.HandleInput();

                        otherScreenHasFocus = true;
                    }

                    // If this is an active non-popup, inform any subsequent
                    // screens that they are covered by it.
                    if (!screen.IsPopup)
                    {
                        coveredByOtherScreen = true;
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

        #region Public Properties

        /// <summary>
        /// Expose an array holding all the screens. We return a copy rather
        /// than the real master list, because screens should only ever be added
        /// or removed using the AddScreen and RemoveScreen methods.
        /// </summary>
        public GameScreen[] Screens
        {
            get
            {
                return screens.ToArray();
            }
        }

        /// <summary>
        /// A default SpriteBatch shared by all the screens. This saves
        /// each screen having to bother creating their own local instance.
        /// </summary>
        public SpriteBatch SpriteBatch
        {
            get
            {
                return spriteBatch;
            }
        }

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

        #endregion Public Properties

        #region Operations

        /// <summary>
        /// Adds a new screen to the screen manager.
        /// </summary>
        public void AddScreen(GameScreen screen)
        {
            screen.IsExiting = false;

            // If we have a graphics device, tell the screen to load content.
            if (isInitialized)
            {
                screen.LoadContent();
            }

            screens.Add(screen);
        }

        /// <summary>
        /// Removes a screen from the screen manager. You should normally
        /// use Screen.ExitScreen instead of calling this directly, so
        /// the screen can gradually transition off rather than just being
        /// instantly removed.
        /// </summary>
        public void RemoveScreen(GameScreen screen)
        {
            // If we have a graphics device, tell the screen to unload content.
            if (isInitialized)
            {
                screen.UnloadContent();
            }

            screens.Remove(screen);
            screensToUpdate.Remove(screen);
        }

        /// <summary>
        /// Prints a list of all the screens, for debugging.
        /// </summary>
        private void TraceScreens()
        {
            List<string> screenNames = new List<string>();

            foreach (GameScreen screen in screens)
            {
                screenNames.Add(screen.GetType().Name);
            }

            Debug.WriteLine(string.Join(", ", screenNames.ToArray()));
        }

        /// <summary>
        /// Helper draws a translucent black fullscreen sprite, used for fading
        /// screens in and out, and for darkening the background behind popups.
        /// </summary>
        public void FadeBackBuffer(float alpha, Color color)
        {
            Viewport viewport = GraphicsDevice.Viewport;

            spriteBatch.Begin();

            spriteBatch.Draw(blankTexture, new Rectangle(0, 0, viewport.Width, viewport.Height), color * alpha);

            spriteBatch.End();
        }

        #endregion Operations
    }
}