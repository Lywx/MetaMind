namespace MetaMind.Acutance.Components
{
    using MetaMind.Acutance.Sessions;
    using MetaMind.Engine;
    using MetaMind.Engine.Components.Events;
    using MetaMind.Engine.Sessions;

    // TODO: Separate SaveManager to 
    public class SaveManager : Engine.Components.SaveManager
    {
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

            // Trigger an event to inform other components to refresh states
            this.GameInterop.Event.TriggerEvent(new Event((int)SessionEventType.SessionSaved, null));
        }

        public override void Load()
        {
            Acutance.Session = Session<SessionData>.LoadSave();
        }
    }
}