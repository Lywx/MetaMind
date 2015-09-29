namespace MetaMind.Engine
{
    using System;
    using Components.Graphics.Adapters;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using MonoGame.Extended.ViewportAdapters;

    /// <summary>
    /// View of MVC pattern in game module.
    /// </summary>
    public class GameComponentVisual<TModule, TModuleSettings, TModuleLogic, TModuleVisual> : GameComponentComponent<TModule, TModuleSettings, TModuleLogic, TModuleVisual>, IGameComponentVisual<TModuleSettings>
        where                        TModule                                                : IGameComponentModule<TModuleSettings, TModuleLogic, TModuleVisual>
        where                        TModuleLogic                                           : IGameComponentLogic<TModuleSettings> 
        where                        TModuleVisual                                          : IGameComponentVisual<TModuleSettings>
    {
        #region Constructors and Finalizer

        protected GameComponentVisual(TModule module, GameEngine engine)
            : base(module, engine)
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