namespace MetaMind.Engine
{
    using System;
    using Microsoft.Xna.Framework;
    using Service;

    /// <summary>
    /// Controller of MVC pattern in GameComponent.
    /// </summary>
    public class MMMvcComponentLogic<TModule, TMvcSettings, TMvcLogic, TMvcVisual> : MMMvcComponentComponent<TModule, TMvcSettings, TMvcLogic, TMvcVisual>, IMMMvcComponentLogic<TMvcSettings>
        where                          TModule                                       : IMMMvcComponent<TMvcSettings, TMvcLogic, TMvcVisual>
        where                          TMvcLogic                                     : IMMMvcComponentLogic<TMvcSettings> 
        where                          TMvcVisual                                    : IMMMvcComponentVisual<TMvcSettings>
    {
        protected MMMvcComponentLogic(TModule module)
            : base(module)
        {
        }

        public virtual void Update(GameTime time)
        {
        }

        public virtual void UpdateInput(IMMEngineInputService input, GameTime time)
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