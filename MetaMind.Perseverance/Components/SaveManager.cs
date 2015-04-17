namespace MetaMind.Perseverance.Components
{
    using MetaMind.Perseverance.Sessions;

    using Microsoft.Xna.Framework;

    public class SaveManager : Engine.Components.SaveManager
    {
        private SaveManager(Game gameEngine)
            : base(gameEngine)
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