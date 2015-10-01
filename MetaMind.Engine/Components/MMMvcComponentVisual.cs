namespace MetaMind.Engine.Components
{
    using System;
    using Components.Graphics.Adapters;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// View of MVC pattern in game module.
    /// </summary>
    public class MMMvcComponentVisual<TMvcComponent, TMvcSettings, TMvcLogic, TMvcVisual> : MMMvcComponentComponent<TMvcComponent, TMvcSettings, TMvcLogic, TMvcVisual>, IMMMvcComponentVisual<TMvcSettings>
        where                         TMvcComponent                                       : IMMMvcComponent<TMvcSettings, TMvcLogic, TMvcVisual>
        where                         TMvcLogic                                     : IMMMvcComponentLogic<TMvcSettings> 
        where                         TMvcVisual                                    : IMMMvcComponentVisual<TMvcSettings>
    {
        #region Constructors and Finalizer

        protected MMMvcComponentVisual(TMvcComponent module)
            : base(module)
        {
            this.ViewportAdapter = new DefaultViewportAdapter(this.GraphicsDevice); 
        }

        #endregion

        #region Graphics Data

        protected ViewportAdapter ViewportAdapter { get; set; }

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