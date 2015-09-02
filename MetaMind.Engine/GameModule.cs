namespace MetaMind.Engine
{
    using System;
    using Microsoft.Xna.Framework;
    using Services;

    public abstract class GameModule<TGroupSettings> : IGameModule<TGroupSettings>
    {
        #region Constructors

        protected GameModule(TGroupSettings settings, GameEngine engine)
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

        public TGroupSettings Settings { get; protected set; }

        public IGameModuleLogic Logic { get; protected set; }

        public IGameModuleVisual Visual { get; protected set; }

        protected GameEngine Engine { get; private set; }

        #region Initialization

        public virtual void Initialize()
        {
        }

        #endregion

        #region Draw

        public void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.Visual?.Draw(graphics, time, alpha);
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