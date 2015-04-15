namespace MetaMind.Acutance.Components
{
    using MetaMind.Acutance.Sessions;
    using MetaMind.Engine;
    using MetaMind.Engine.Components.Events;

    public class SaveManager : Engine.Components.SaveManager
    {
        #region Singleton

        private static SaveManager Singleton { get; set; }

        public static SaveManager GetInstance(GameEngine gameEngine)
        {
            if (Singleton == null)
            {
                Singleton = new SaveManager(gameEngine);
            }

            if (gameEngine != null)
            {
                gameEngine.Components.Add(Singleton);
            }

            return Singleton;
        }

        #endregion Singleton

        #region Engine Data

        private IGameInterop GameInterop { get; set; }

        #endregion

        #region Constructors

        private SaveManager(GameEngine gameEngine)
            : base(gameEngine)
        {
            this.GameInterop = new GameEngineInterop(gameEngine);
        }

        #endregion

        public override void Save()
        {
            Acutance.Session.Save();

            // trigger an event to inform other components to refresh states
            this.GameInterop.Events.TriggerEvent(new Event((int)SessionEventType.SessionSaved, null));
        }

        public override void Load()
        {
            Acutance.Session = Session.LoadSave();
        }
    }
}