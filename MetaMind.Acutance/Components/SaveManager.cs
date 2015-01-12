namespace MetaMind.Acutance.Components
{
    using MetaMind.Acutance.Sessions;
    using MetaMind.Engine;
    using MetaMind.Engine.Components;
    using MetaMind.Engine.Components.Events;

    using Game = Microsoft.Xna.Framework.Game;

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

            // trigger an event to inform other components to refresh states
            GameEngine.EventManager.TriggerEvent(new EventBase((int)SessionEventType.SessionSaved, null));
        }

        public override void Load()
        {
            Acutance.Session = Session.LoadSave();
        }
    }
}