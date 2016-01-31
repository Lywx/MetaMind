namespace MetaMind.Session.Components
{
    using Engine;
    using Engine.Core;
    using Engine.Core.Backend.Interop;
    using Engine.Core.Sessions;

    public class MMSessionSaveManager : MMSaveManager
    {
        public MMSessionSaveManager(MMEngine engine)
            : base(engine)
        {
        }

        #region Operations

        public override void Save()
        {
            MMSessionGame.Session.Save();
        }

        public override void Load()
        {
            MMSessionGame.Session = MMSession<MMSessionGameData, MMSessionController>.Load();
        }

        #endregion Operations
    }
}