namespace MetaMind.Session.Components
{
    using Engine;
    using Engine.Components.Interop;
    using Engine.Sessions;
    using Sessions;

    public class SessionSaveManager : MMSaveManager
    {
        public SessionSaveManager(MMEngine engine)
            : base(engine)
        {
        }

        #region Operations

        public override void Save()
        {
            SessionGame.Session.Save();
        }

        public override void Load()
        {
            SessionGame.Session = MMSession<SessionData, SessionController>.Load();
        }

        #endregion Operations
    }
}