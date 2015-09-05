namespace MetaMind.Engine
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class GameModuleVisual<TModule, TModuleSettings, TModuleLogic, TModuleVisual> : GameModuleComponent<TModule, TModuleSettings, TModuleLogic, TModuleVisual>, IGameModuleVisual<TModuleSettings>
        where                     TModule                                                : IGameModule<TModuleSettings, TModuleLogic, TModuleVisual>
        where                     TModuleLogic                                           : IGameModuleLogic<TModuleSettings> 
        where                     TModuleVisual                                          : IGameModuleVisual<TModuleSettings>
    {
        #region Constructors and Finalizer

        protected GameModuleVisual(TModule module, GameEngine engine)
            : base(module, engine)
        {
        }

        #endregion

        #region Update

        public virtual void Update(GameTime time)
        {
        }

        #endregion

        #region Draw

        public virtual void BeginDraw(GameTime time)
        {
            
        }

        public virtual void Draw(GameTime time)
        {
        }

        public virtual void EndDraw(GameTime time)
        {
        }

        #endregion

        #region IDisposable

        private bool IsDisposed { get; set; }

        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) { }

        #endregion
    }
}