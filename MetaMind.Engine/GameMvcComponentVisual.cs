namespace MetaMind.Engine
{
    using System;
    using Components.Graphics.Adapters;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// View of MVC pattern in game module.
    /// </summary>
    public class GameMvcComponentVisual<TModule, TMvcSettings, TMvcLogic, TMvcVisual> : GameMvcComponentComponent<TModule, TMvcSettings, TMvcLogic, TMvcVisual>, IGameMvcComponentVisual<TMvcSettings>
        where                        TModule                                                : IGameMvcComponent<TMvcSettings, TMvcLogic, TMvcVisual>
        where                        TMvcLogic                                           : IGameMvcComponentLogic<TMvcSettings> 
        where                        TMvcVisual                                          : IGameMvcComponentVisual<TMvcSettings>
    {
        #region Constructors and Finalizer

        protected GameMvcComponentVisual(TModule module)
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