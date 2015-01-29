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
        public Acutance(GameEngine engine, SynchronizationServiceClient synchronization)
            : base(engine)
        {
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

            GameEngine.ScreenManager.AddScreen(new BackgroundScreen());
            GameEngine.ScreenManager.AddScreen(new MultiplexerScreen());
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