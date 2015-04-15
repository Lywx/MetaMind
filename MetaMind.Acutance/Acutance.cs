namespace MetaMind.Acutance
{
    using System.Speech.Synthesis;

    using MetaMind.Acutance.PerseveranceServiceReference;
    using MetaMind.Acutance.Screens;
    using MetaMind.Acutance.Sessions;
    using MetaMind.Engine;

    using Microsoft.Xna.Framework;

    using Game = MetaMind.Engine.Game;
    using SaveManager = MetaMind.Acutance.Components.SaveManager;

    public class Acutance : Game
    {
        public Acutance(GameEngine gameEngine, SynchronizationServiceClient synchronization)
            : base(gameEngine)
        {
            // components
            //----------------------------------------------------------------- 
            Synchronization = synchronization;

            // domain specific save manager
            SaveManager = SaveManager.GetInstance(gameEngine);

            // speech synthesizer
            Synthesizer = new SpeechSynthesizer { Volume = 100, Rate = 3 };

            // save resource mode
            this.GameInterop.GameEngine.Screens.Settings.IsAlwaysActive = false;
            this.GameInterop.GameEngine.Screens.Settings.IsAlwaysVisible = false;
        }

        #region Components

        // FIXME: static ?

        public static Session Session { get; set; }

        public static SynchronizationServiceClient Synchronization { get; private set; }

        public static SpeechSynthesizer Synthesizer { get; private set; }

        private static SaveManager SaveManager { get; set; }

        #endregion Components

        #region Initialization

        public override void Initialize()
        {
            base.Initialize();

            SaveManager.Load();

            GameEngine.Screens.AddScreen(new BackgroundScreen());
            GameEngine.Screens.AddScreen(new MultiplexerScreen());
        }

        #endregion Initialization

        #region Update

        public override void Update(GameTime gameTime)
        {
            Session.Update();

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