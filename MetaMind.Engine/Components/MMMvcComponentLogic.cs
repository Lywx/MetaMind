namespace MetaMind.Engine.Components
{
    using System;
    using Microsoft.Xna.Framework;
    using Services;

    /// <summary>
    /// Controller of MVC pattern in GameComponent.
    /// </summary>
    public class MMMvcComponentLogic<TMvcComponent, TMvcSettings, TMvcLogic, TMvcVisual> : MMMvcComponentComponent<TMvcComponent, TMvcSettings, TMvcLogic, TMvcVisual>, IMMMvcComponentLogic<TMvcSettings>
        where                        TMvcComponent                                       : IMMMvcComponent<TMvcSettings, TMvcLogic, TMvcVisual>
        where                        TMvcLogic                                     : IMMMvcComponentLogic<TMvcSettings> 
        where                        TMvcVisual                                    : IMMMvcComponentVisual<TMvcSettings>
    {
        protected MMMvcComponentLogic(TMvcComponent module)
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