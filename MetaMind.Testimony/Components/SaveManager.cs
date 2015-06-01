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
            Testimony.Session.Save();
        }

        public override void Load()
        {
            Testimony.Session = Session<SessionData>.Load();
            Testimony.SessionData = Testimony.Session.Data;
        }

        #endregion Operations
    }
}