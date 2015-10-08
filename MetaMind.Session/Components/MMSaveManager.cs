namespace MetaMind.Session.Components
{
    using Engine;
    using Engine.Sessions;
    using Session.Sessions;

    public class MMSaveManager : Engine.Components.Interop.MMSaveManager
    {
        public MMSaveManager(MMEngine engine)
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