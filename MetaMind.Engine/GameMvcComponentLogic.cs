namespace MetaMind.Engine
{
    using System;
    using Microsoft.Xna.Framework;
    using Service;

    /// <summary>
    /// Controller of MVC pattern in GameComponent.
    /// </summary>
    public class GameMvcComponentLogic<TModule, TMvcSettings, TMvcLogic, TMvcVisual> : GameMvcComponentComponent<TModule, TMvcSettings, TMvcLogic, TMvcVisual>, IGameMvcComponentLogic<TMvcSettings>
        where                          TModule                                       : IGameMvcComponent<TMvcSettings, TMvcLogic, TMvcVisual>
        where                          TMvcLogic                                     : IGameMvcComponentLogic<TMvcSettings> 
        where                          TMvcVisual                                    : IGameMvcComponentVisual<TMvcSettings>
    {
        protected GameMvcComponentLogic(TModule module)
            : base(module)
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