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
                return now.Minute == 59 || now.Minute == 14 ||
                       now.Minute == 29 || now.Minute == 44;
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