// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Screens.cs">
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

    public class MMScreenDirector : MMInputableComponent, IMMScreenDirector
    {
        #region Dependency

        public MMScreenSettings Settings { get; set; }

        /// <summary>
        /// A default SpriteBatch shared by all the screens. This saves
        /// each screen having to bother creating their own local instance.
        /// </summary>
        private SpriteBatch SpriteBatch { get; set; }

        #endregion

        #region Screen Data

        private readonly List<IMMScreen> screens = new List<IMMScreen>();

        private readonly List<IMMScreen> screensToUpdate = new List<IMMScreen>();

        /// <summary>
        /// Expose an array holding all the screens. We return a copy rather
        /// than the real master list, because screens should only ever be added
        /// or removed using the AddScreen and RemoveScreen methods.
        /// </summary>
        public IMMScreen[] Screens => this.screens.ToArray();

        #endregion Screen Data

        #region State Data

        private bool initialized;

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
        public MMScreenDirector(MMEngine engine, MMScreenSettings settings, SpriteBatch spriteBatch)
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
            this.initialized = true;

            base.Initialize();            
        }

        #endregion Initialization

        #region Load and Unload

        protected override void LoadContent()
        {
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
                // When the game window is inactive, don't draw to save resources
                this.Game.IsActive ||

                // Or 
                this.Settings.AlwaysDraw)
            {
                var visible = new Predicate<IMMScreen>(screen => screen.ScreenState != MMScreenState.Hidden);

                // Draw texture into render target
                this.FindScreens(visible).ForEach(screen => screen.BeginDraw(this.Graphics, time));

                // Draw render target into back buffer
                this.FindScreens(visible).ForEach(screen => screen.EndDraw(this.Graphics, time));
            }
        }

        /// <summary>
        /// Allows each screen to run logic.
        /// </summary>
        public override void Update(GameTime time)
        {
            // When the game window is active, update the screen because users 
            // actually use it
            if (this.Game.IsActive || 

                // Or
                this.Settings.AlwaysActive)
            {
                this.UpdateScreensStates(time);
                this.UpdateScreens(time);
            }
        }

        private void UpdateScreens(GameTime time)
        {
            this.FindScreens(screen => true).ForEach(screen => screen.Update(time));
        }

        /// <summary>
        /// Update screen states.
        /// </summary>
        /// <param name="time"></param>
        private void UpdateScreensStates(GameTime time)
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

                if (screen.ScreenState == MMScreenState.TransitionOn ||
                    screen.ScreenState == MMScreenState.Active)
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

        public override void UpdateInput(GameTime time)
        {
            this.FindScreens(screen => screen.IsActive)
                .ForEach(screen => screen.UpdateInput(this.Input, time));
        }

        #endregion Update and Draw

        #region Operations

        public void AddScreen(IMMScreen screen)
        {
            ((MMScreen)screen).IsExiting = false;

            // If we have a graphics device, tell the screen to load content.
            if (this.initialized)
            {
                screen.LoadContent(this.Interop);
            }

            this.screens.Add(screen);
        }

        public void ExitScreen(IMMScreen screen)
        {
            screen.Exit();
        }

        public void ExitScreenFrom(int index)
        {
            this.screens.Skip(index).ToList().ForEach(s => s.Exit());
        }

        private List<IMMScreen> FindScreens(Predicate<IMMScreen> predicate)
        {
            return this.screens.FindAll(predicate);
        }

        public void RemoveScreen(IMMScreen screen)
        {
            // If we have a graphics device, tell the screen to unload content.
            if (this.initialized)
            {
                screen.UnloadContent(this.Interop);
            }

            this.screens.Remove(screen);
            this.screensToUpdate.Remove(screen);
        }

        public void ReplaceScreen(IMMScreen screen)
        {
            if (this.screens.Count != 0)
            {
                this.RemoveScreen(this.screens[this.screens.Count]);
            }

            this.AddScreen(screen);
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

        #region Events

        /// <summary>
        /// Called when the MMGame Engine is closed by Exit / Restart operations.
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

                // Note that sprite batch is not owned by this
                this.SpriteBatch = null;
            }

            base.Dispose(disposing);
        }

        #endregion
    }
}