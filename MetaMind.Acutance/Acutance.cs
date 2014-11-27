namespace MetaMind.Acutance
{
    using System;

    using MetaMind.Acutance.Components;
    using MetaMind.Acutance.PerseveranceServiceReference;
    using MetaMind.Acutance.Screens;
    using MetaMind.Acutance.Sessions;
    using MetaMind.Engine;
    using MetaMind.Engine.Settings;

    using Microsoft.Xna.Framework;

    public class Acutance : EngineRunner
    {
        private const int Fps = 30;

        public Acutance(GameEngine engine, SynchronizationServiceClient synchronization, bool fullscreen)
            : base(engine)
        {
            GraphicsSettings.Fullscreen = fullscreen;

            if (fullscreen)
            {
                GameEngine.Window.IsBorderless = true;
            }

            // show mouse
            GameEngine.IsMouseVisible = true;

            // Time step
            GameEngine.TargetElapsedTime = TimeSpan.FromMilliseconds(1000 / (double)Fps);
            GameEngine.IsFixedTimeStep   = true;

            // faster update loop but higher cpu usage
            //// GameEngine.IsFixedTimeStep = false;

            // center game window
            var screen = GraphicsSettings.Screen;
            GameEngine.Window.Position = new Point(
                screen.Bounds.X + (screen.Bounds.Width  - GraphicsSettings.Width)  / 2, 
                screen.Bounds.Y + (screen.Bounds.Height - GraphicsSettings.Height) / 2);

            // set width and height
            GameEngine.GraphicsManager.PreferredBackBufferWidth  = GraphicsSettings.Width;
            GameEngine.GraphicsManager.PreferredBackBufferHeight = GraphicsSettings.Height;
            GameEngine.GraphicsManager.ApplyChanges();

            // components
            //----------------------------------------------------------------- 
            Synchronization = synchronization;
            SaveManager = SaveManager.GetInstance(Game);
        }

        #region Components

        public static Adventure Adventure { get; private set; }

        public static SynchronizationServiceClient Synchronization { get; private set; }

        private static SaveManager SaveManager { get; set; }

        #endregion Components

        #region Initialization

        public override void Initialize()
        {
            base.Initialize();

            Adventure = Adventure.LoadSave();

            GameEngine.ScreenManager.AddScreen(new BackgroundScreen());
            GameEngine.ScreenManager.AddScreen(new MultiplexerScreen());
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