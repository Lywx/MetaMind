namespace MetaMind.Testimony.Components
{
    using MetaMind.Engine;
    using MetaMind.Engine.Sessions;
    using MetaMind.Testimony.Sessions;

    public class SaveManager : Engine.Components.SaveManager
    {
        public SaveManager(GameEngine engine)
            : base(engine)
        {
        }

        #region Operations

        public override void Save()
        {
            Runtime.Session.Save();
        }

        public override void Load()
        {
            Runtime.Session = Session<SessionData>.Load();
            Runtime.SessionData = Runtime.Session.Data;
        }

        #endregion Operations
    }
}