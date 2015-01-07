namespace MetaMind.Engine.Components
{
    using System;

    using Microsoft.Xna.Framework;

    public class SaveManager : GameComponent
    {
        private bool autoSaved;

        protected SaveManager(Game game)
            : base(game)
        {
        }

        private bool AutoSaveCondition
        {
            get
            {
                var now = DateTime.Now;
                return now.Minute == 0 || now.Minute == 15 ||
                       now.Minute == 30 || now.Minute == 45;
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (this.AutoSaveCondition)
            {
                this.AutoSave();
            }
            else
            {
                this.autoSaved = false;
            }

            base.Update(gameTime);
        }

        private void AutoSave()
        {
            if (this.autoSaved)
            {
                return;
            }

            this.Save();

            this.autoSaved = true;
        }

        public virtual void Save()
        {
        }

        public virtual void Load()
        {
        }
    }
}