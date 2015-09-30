namespace MetaMind.Engine
{
    using System;
    using Components.Graphics.Adapters;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// View of MVC pattern in game module.
    /// </summary>
    public class MMMvcComponentVisual<TModule, TMvcSettings, TMvcLogic, TMvcVisual> : MMMvcComponentComponent<TModule, TMvcSettings, TMvcLogic, TMvcVisual>, IMMMvcComponentVisual<TMvcSettings>
        where                        TModule                                                : IMMMvcComponent<TMvcSettings, TMvcLogic, TMvcVisual>
        where                        TMvcLogic                                           : IMMMvcComponentLogic<TMvcSettings> 
        where                        TMvcVisual                                          : IMMMvcComponentVisual<TMvcSettings>
    {
        #region Constructors and Finalizer

        protected MMMvcComponentVisual(TModule module)
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