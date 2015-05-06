namespace MetaMind.Acutance
{
    using System.Speech.Synthesis;

    using MetaMind.Acutance.Screens;
    using MetaMind.Acutance.Sessions;
    using MetaMind.Engine;
    using MetaMind.Engine.Sessions;

    using Microsoft.Xna.Framework;

    using Game = MetaMind.Engine.Game;
    using SaveManager = MetaMind.Acutance.Components.SaveManager;

    public class Acutance : Game
    {
        public Acutance(GameEngine gameEngine)
            : base(gameEngine)
        {
            // domain specific save manager
            this.SaveManager = new SaveManager(gameEngine);

            // speech synthesizer
            Synthesizer = new SpeechSynthesizer { Volume = 100, Rate = 3 };

            // save resource mode
            this.Interop.Screen.Settings.IsAlwaysActive = false;
            this.Interop.Screen.Settings.IsAlwaysVisible = false;
        }

        #region Components

        // FIXME: static ?

        public static Session<SessionData> Session { get; set; }

        public static SpeechSynthesizer Synthesizer { get; private set; }

        private SaveManager SaveManager { get; set; }

        #endregion Components

        #region Initialization

        public override void Initialize()
        {
            base.Initialize();

            SaveManager.Load();

            this.Interop.Screen.AddScreen(new MultiplexerScreen());
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