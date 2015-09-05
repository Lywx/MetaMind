namespace MetaMind.Engine
{
    using System;
    using Microsoft.Xna.Framework;
    using Services;

    public abstract class GameModule<TModuleSettings, TModuleLogic, TModuleVisual> : IGameModule<TModuleSettings, TModuleLogic, TModuleVisual>, IDisposable
        where TModuleLogic                                                         : IGameModuleLogic<TModuleSettings>
        where TModuleVisual                                                        : IGameModuleVisual<TModuleSettings>
    {
        #region Constructors

        protected GameModule(TModuleSettings settings, GameEngine engine)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            if (engine == null)
            {
                throw new ArgumentNullException(nameof(engine));
            }

            this.Settings = settings;
            this.Engine   = engine;
        }

        ~GameModule()
        {
            this.Dispose(true);
        }

        #endregion

        public TModuleSettings Settings { get; protected set; }

        public TModuleLogic Logic { get; protected set; }

        public TModuleVisual Visual { get; protected set; }

        protected GameEngine Engine { get; private set; }

        #region Initialization

        public virtual void Initialize()
        {
        }

        #endregion

        #region Draw

        public void BeginDraw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.Visual?.BeginDraw(graphics, time, alpha);
        }

        public void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.Visual?.Draw(graphics, time, alpha);
        }

        public void EndDraw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.Visual?.EndDraw(graphics, time, alpha);
        }

        #endregion

        #region Update

        public void UpdateInput(IGameInputService input, GameTime time)
        {
            this.Logic ?.Update(time);
            this.Visual?.Update(time);
        }

        public void Update(GameTime time)
        {
            this.Logic ?.Update(time);
            this.Visual?.Update(time);
        }

        #endregion

        #region IDisposable

        protected virtual void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(this);
        }

        #endregion
    }
}