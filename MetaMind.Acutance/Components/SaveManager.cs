namespace MetaMind.Acutance.Components
{
    using MetaMind.Acutance.Sessions;
    using MetaMind.Engine;
    using MetaMind.Engine.Components.Events;
    using MetaMind.Engine.Services;
    using MetaMind.Engine.Sessions;

    public class SaveManager : Engine.Components.SaveManager
    {
        #region Dependency

        private IGameInteropService Interop { get; set; }

        #endregion

        #region Constructors

        public SaveManager(GameEngine engine)
            : base(engine)
        {
            this.Interop = GameEngine.Service.Interop;
        }

        #endregion

        public override void Save()
        {
            Acutance.Session.Save();

            // Trigger an event to inform other components to refresh states
            this.Interop.Event.TriggerEvent(new Event((int)SessionEventType.SessionSaved, null));
        }

        public override void Load()
        {
            Acutance.Session = Session<SessionData>.Load();
        }
    }
}