namespace MetaMind.Acutance
{
    using System;

    using MetaMind.Acutance.PerseveranceServiceReference;
    using MetaMind.Acutance.Screens;
    using MetaMind.Acutance.Sessions;
    using MetaMind.Engine;
    using MetaMind.Engine.Settings;

    using Microsoft.Xna.Framework;

    public class Acutance : EngineRunner
    {
        private const int Fps = 30;

        public Acutance(GameEngine engine, SynchronizationServiceClient synchronization)
            : base(engine)
        {
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

            // components
            //----------------------------------------------------------------- 
            Synchronization = synchronization;
        }

        #region Components

        public static SynchronizationServiceClient Synchronization { get; private set; }

        #endregion Components

        #region Initialization

        public override void Initialize()
        {
            base.Initialize();

            Adventure.LoadSave();

            GameEngine.ScreenManager.AddScreen(new BackgroundScreen());
            GameEngine.ScreenManager.AddScreen(new MultiplexerScreen());
        }

        #endregion Initialization

        #region System

        public override void OnExiting()
        {
        }

        #endregion System
    }
}