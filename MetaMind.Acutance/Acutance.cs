namespace MetaMind.Acutance
{
    using MetaMind.Acutance.Components;
    using MetaMind.Acutance.PerseveranceServiceReference;
    using MetaMind.Acutance.Screens;
    using MetaMind.Acutance.Sessions;
    using MetaMind.Engine;
    using MetaMind.Engine.Settings;

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