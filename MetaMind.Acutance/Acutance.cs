namespace MetaMind.Acutance
{
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Speech.Synthesis;

    using MetaMind.Acutance.PerseveranceServiceReference;
    using MetaMind.Acutance.Screens;
    using MetaMind.Acutance.Sessions;
    using MetaMind.Engine;
    using MetaMind.Engine.Components;
    using MetaMind.Engine.Settings;

    using Microsoft.Xna.Framework;

    using SaveManager = MetaMind.Acutance.Components.SaveManager;

    public class Acutance : EngineRunner
    {
        public Acutance(GameEngine engine, SynchronizationServiceClient synchronization, bool fullscreen)
            : base(engine)
        {
            GameEngine.Fps = 30;

            GraphicsSettings.Fullscreen = fullscreen;

            if (fullscreen)
            {
                GameEngine.Window.IsBorderless = true;
            }

            GameEngine.IsMouseVisible = true;

            GameEngine.TriggerCenter();

            // components
            //----------------------------------------------------------------- 
            Synchronization = synchronization;

            // domain specific save manager
            SaveManager = SaveManager.GetInstance(Game);

            // speech synthesizer
            Synthesizer = new SpeechSynthesizer { Volume = 100, Rate = 3 };

            // save resource mode
            GameEngine.ScreenManager.Settings.AlwaysUpdate = false;
            GameEngine.ScreenManager.Settings.AlwaysDraw = false;
        }

        #region Components

        public static Adventure Adventure { get; private set; }

        public static SynchronizationServiceClient Synchronization { get; private set; }

        public static SpeechSynthesizer Synthesizer { get; private set; }

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

        #region Update

        public override void Update(GameTime gameTime)
        {
            Adventure.Update();

            base.Update(gameTime);
        }

        #endregion

        #region System

        public override void OnExiting()
        {
            SaveManager.Save();
        }

        #endregion System
    }
}