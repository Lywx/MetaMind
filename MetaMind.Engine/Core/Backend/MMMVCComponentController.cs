namespace MetaMind.Engine.Core.Backend
{
    using System;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Controller of MVC pattern in GameComponent.
    /// </summary>
    public class MMMVCComponentController<TMVCComponent, TMVCSettings, TMVCController, TMVCRenderer> : MMMVCComponentComponent<TMVCComponent, TMVCSettings, TMVCController, TMVCRenderer>, IMMMVCComponentController<TMVCSettings>
        where                        TMVCComponent                                                   : IMMMVCComponent<TMVCSettings, TMVCController, TMVCRenderer>
        where                        TMVCController                                                  : IMMMVCComponentController<TMVCSettings> 
        where                        TMVCRenderer                                                    : IMMMVCComponentRenderer<TMVCSettings>
    {
        protected MMMVCComponentController(TMVCComponent module)
            : base(module)
        {
        }

        public virtual void Update(GameTime time)
        {
        }

        public virtual void UpdateInput(GameTime time)
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