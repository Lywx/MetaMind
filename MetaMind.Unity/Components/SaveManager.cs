namespace MetaMind.Unity.Components
{
    using Engine;
    using Engine.Session;
    using Sessions;

    public class SaveManager : Engine.Components.Interop.SaveManager
    {
        public SaveManager(MMEngine engine)
            : base(engine)
        {
        }

        #region Operations

        public override void Save()
        {
            Unity.Session.Save();
        }

        public override void Load()
        {
            Unity.Session = Session<SessionData, SessionController>.Load();
        }

        #endregion Operations
    }
}