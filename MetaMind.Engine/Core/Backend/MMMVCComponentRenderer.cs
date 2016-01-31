namespace MetaMind.Engine.Core.Backend
{
    using System;
    using Entity.Graphics.Adapters;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// View of MVC pattern in game module.
    /// </summary>
    public class MMMVCComponentRenderer<TMVCComponent, TMVCSettings, TMVCController, TMVCRenderer> : MMMVCComponentComponent<TMVCComponent, TMVCSettings, TMVCController, TMVCRenderer>, IMMMVCComponentRenderer<TMVCSettings>
        where                           TMVCComponent                                              : IMMMVCComponent<TMVCSettings, TMVCController, TMVCRenderer>
        where                           TMVCController                                             : IMMMVCComponentController<TMVCSettings> 
        where                           TMVCRenderer                                               : IMMMVCComponentRenderer<TMVCSettings>
    {
        #region Constructors and Finalizer

        protected MMMVCComponentRenderer(TMVCComponent module)
            : base(module)
        {
            this.ViewportAdapter = new MMDefaultViewportAdapter(); 
        }

        #endregion

        #region Graphics Data

        protected MMViewportAdapter ViewportAdapter { get; set; }

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

        #region Update

        public virtual void Update(GameTime time)
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