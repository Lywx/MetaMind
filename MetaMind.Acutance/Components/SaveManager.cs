namespace MetaMind.Acutance.Components
{
    using MetaMind.Acutance.Sessions;

    using Microsoft.Xna.Framework;

    public class SaveManager : Engine.Components.SaveManager
    {
        #region Singleton

        private static SaveManager singleton;

        public static SaveManager GetInstance(Game game)
        {
            if (singleton == null)
            {
                singleton = new SaveManager(game);
            }

            if (game != null)
            {
                game.Components.Add(singleton);
            }

            return singleton;
        }

        #endregion Singleton

        private SaveManager(Game game)
            : base(game)
        {
        }

        public override void Save()
        {
            Acutance.Session.Save();
        }

        public override void Load()
        {
            Acutance.Session = Session.LoadSave();
        }
    }
}