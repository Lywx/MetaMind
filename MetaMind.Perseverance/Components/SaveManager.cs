namespace MetaMind.Runtime.Components
{
    using MetaMind.Engine;
    using MetaMind.Engine.Services;
    using MetaMind.Engine.Sessions;
    using MetaMind.Runtime.Sessions;

    public class SaveManager : Engine.Components.SaveManager
    {
        public SaveManager(GameEngine engine)
            : base(engine)
        {
        }

        public override void Initialize()
        {
            // Dependency
            this.Interop = GameEngine.Service.Interop;

            base.Initialize();
        }

        #region Dependency

        private IGameInteropService Interop { get; set; }

        #endregion


        #region Operations

        public override void Save()
        {
            Runtime.Session.Save();
        }

        public override void Load()
        {
            Runtime.Session = Session<SessionData>.Load();
            Runtime.SessionData = Runtime.Session.Data
        }

        #endregion Operations
    }
}