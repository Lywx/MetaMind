namespace MetaMind.Perseverance.Components
{
    using MetaMind.Engine;
    using MetaMind.Perseverance.Sessions;

    public class SaveManager : Engine.Components.SaveManager
    {
        private SaveManager(GameEngine engine)
            : base(engine)
        {
        }

        #region Operations

        public override void Save()
        {
            Perseverance.Session.Save();
        }

        public override void Load()
        {
            Perseverance.Session = Session.LoadSave();
        }

        #endregion Operations
    }
}