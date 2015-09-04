namespace MetaMind.Engine
{
    using System;
    using Microsoft.Xna.Framework;
    using Services;

    public abstract class GameControllableComponent : DrawableGameComponent, IGameControllableComponent
    {
        #region Constructors

        protected GameControllableComponent(GameEngine engine)
            : base(engine)
        {
            if (engine == null)
            {
                throw new ArgumentNullException(nameof(engine));
            }

            this.Engine = engine;
        }

        #endregion

        #region Engine Data

        protected IGameInputService Input => GameEngine.Service.Input;

        protected IGameInteropService Interop => GameEngine.Service.Interop;

        protected IGameGraphicsService Graphics => GameEngine.Service.Graphics;

        protected IGameNumericalService Numerical => GameEngine.Service.Numerical;

        public GameEngine Engine { get; protected set; }

        #endregion Engine Data

        public virtual void UpdateInput(GameTime time) { }
    }
}