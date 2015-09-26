namespace MetaMind.Engine
{
    using System;
    using Microsoft.Xna.Framework;
    using Service;

    /// <summary>
    /// Controller of MVC pattern in game module.
    /// </summary>
    public class GameComponentLogic<TModule, TModuleSettings, TModuleLogic, TModuleVisual> : GameComponentComponent<TModule, TModuleSettings, TModuleLogic, TModuleVisual>, IGameComponentLogic<TModuleSettings>
        where                       TModule                                                : IGameComponentModule<TModuleSettings, TModuleLogic, TModuleVisual>
        where                       TModuleLogic                                           : IGameComponentLogic<TModuleSettings> 
        where                       TModuleVisual                                          : IGameComponentVisual<TModuleSettings>
    {
        protected GameComponentLogic(TModule module, GameEngine engine)
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