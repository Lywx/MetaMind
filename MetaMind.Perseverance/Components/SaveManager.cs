namespace MetaMind.Perseverance.Components
{
    using MetaMind.Perseverance.Sessions;

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

        #region Constructors

        private SaveManager(Game game)
            : base(game)
        {
        }

        #endregion Constructors

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