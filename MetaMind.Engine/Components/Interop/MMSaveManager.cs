namespace MetaMind.Engine.Components.Interop
{
    using System;
    using Microsoft.Xna.Framework;

    public abstract class MMSaveManager : GameComponent, IMMSaveManager
    {
        private bool isAutoSaved;

        #region Constructors

        protected MMSaveManager(MMEngine engine)
            : base(engine)
        {
            if (engine == null)
            {
                throw new ArgumentNullException(nameof(engine));
            }
        }

        #endregion

        private bool AutoSaveCondition
        {
            get
            {
                var now = DateTime.Now;
                return now.Second == 55 && 
                      (now.Minute == 13 || 
                       now.Minute == 28 || 
                       now.Minute == 43 || 
                       now.Minute == 58);
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
                this.isAutoSaved = false;
            }

            base.Update(gameTime);
        }

        private void AutoSave()
        {
            if (this.isAutoSaved)
            {
                return;
            }

            this.Save();

            this.isAutoSaved = true;
        }

        public virtual void Save()
        {
        }

        public virtual void Load()
        {
        }
    }
}