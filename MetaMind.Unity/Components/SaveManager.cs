namespace MetaMind.Unity.Components
{
    using Engine;
    using Engine.Sessions;
    using Sessions;

    public class SaveManager : Engine.Components.SaveManager
    {
        public SaveManager(GameEngine engine)
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
            Unity.Session     = Session<SessionData>.Load();
            Unity.SessionData = Unity.Session.Data;
        }

        #endregion Operations
    }
}