using MetaMind.Engine;
using Microsoft.Xna.Framework;
using System;

namespace MetaMind.Perseverance.Components
{
    public class SaveManager : GameComponent
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

        #region Save Data

        private bool autoSaved;

        private bool AutoSaveCondition
        {
            get
            {
                var now = DateTime.Now;
                return now.Minute == 0 || now.Minute == 15 ||
                       now.Minute == 30 || now.Minute == 45;
            }
        }

        #endregion Save Data

        #region Constructors

        private SaveManager(Game game)
            : base(game)
        {
        }

        #endregion Constructors

        #region Update

        public override void Update(GameTime gameTime)
        {
            if (AutoSaveCondition)
            {
                AutoSave();
            }
            else
            {
                autoSaved = false;
            }

            base.Update(gameTime);
        }

        #endregion Update

        #region Operations

        private void AutoSave()
        {
            if (autoSaved)
            {
                return;
            }

            Perseverance.Adventure.Save();
            GameEngine.MessageManager.PopMessages("Progress saved.");

            autoSaved = true;
        }

        public void Save()
        {
            Perseverance.Adventure.Save();
        }

        #endregion Operations
    }
}