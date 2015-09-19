namespace MetaMind.Engine
{
    using System;
    using Microsoft.Xna.Framework;
    using Service;

    /// <summary>
    /// Controller of MVC pattern in game module.
    /// </summary>
    public class GameModuleLogic<TModule, TModuleSettings, TModuleLogic, TModuleVisual> : GameModuleComponent<TModule, TModuleSettings, TModuleLogic, TModuleVisual>, IGameModuleLogic<TModuleSettings>
        where                    TModule                                 : IGameModule<TModuleSettings, TModuleLogic, TModuleVisual>
        where                    TModuleLogic                            : IGameModuleLogic<TModuleSettings> 
        where                    TModuleVisual                           : IGameModuleVisual<TModuleSettings>
    {
        protected GameModuleLogic(TModule module, GameEngine engine)
            : base(module, engine)
        {
        }

        public virtual void Update(GameTime time)
        {
        }

        public virtual void UpdateInput(IGameInputService input, GameTime time)
        {
        }

        #region IDisposable

        public void Dispose()
        {
            this.Dispose(true);
            
            GC.SuppressFinalize(this);
        }

        private bool IsDisposed { get; set; }

        protected virtual void Dispose(bool disposing) { }

        #endregion
    }
}