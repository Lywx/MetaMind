namespace MetaMind.Acutance
{
    using System;

    using MetaMind.Engine;
    using MetaMind.Engine.Settings;
    using MetaMind.Perseverance.Components;
    using MetaMind.Perseverance.Screens;
    using MetaMind.Perseverance.Sessions;

    using Microsoft.Xna.Framework;

    public class Acutance : EngineRunner
    {
        private const int Fps = 60;

        public Acutance(GameEngine engine, bool fullscreen)
            : base(engine)
        {
            GraphicsSettings.Fullscreen = fullscreen;

            if (fullscreen)
            {
                this.GameEngine.Window.IsBorderless = true;
            }

            // show mouse
            this.GameEngine.IsMouseVisible = true;

            // Time step
            this.GameEngine.TargetElapsedTime = TimeSpan.FromMilliseconds(1000 / (double)Fps);
            this.GameEngine.IsFixedTimeStep   = true;

            // faster update loop but higher cpu usage
            //// GameEngine.IsFixedTimeStep = false;

            // center game window
            var screen = GraphicsSettings.Screen;
            this.GameEngine.Window.Position = new Point(
                screen.Bounds.X + (screen.Bounds.Width  - GraphicsSettings.Width)  / 2, 
                screen.Bounds.Y + (screen.Bounds.Height - GraphicsSettings.Height) / 2);

            // set width and height
            GameEngine.GraphicsManager.PreferredBackBufferWidth  = GraphicsSettings.Width;
            GameEngine.GraphicsManager.PreferredBackBufferHeight = GraphicsSettings.Height;
            GameEngine.GraphicsManager.ApplyChanges();

            // save
            // -----------------------------------------------------------------
            SaveManager = SaveManager.GetInstance(this.Game);
        }

        #region Components

        public static Adventure Adventure { get; private set; }

        private static SaveManager SaveManager { get; set; }

        #endregion Components

        #region Initialization

        public override void Initialize()
        {
            base.Initialize();

            Adventure = Adventure.LoadSave();

            // update adventure time related data
            Adventure.Update();

            GameEngine.ScreenManager.AddScreen(new BackgroundScreen());

            if (Adventure.Cognition.Awake)
            {
                GameEngine.ScreenManager.AddScreen(new MotivationScreen());
            }
            else
            {
                GameEngine.ScreenManager.AddScreen(new SummaryScreen());
            }
        }

        #endregion Initialization

        #region System

        public override void OnExiting()
        {
            SaveManager.Save();
        }

        #endregion System
    }
}