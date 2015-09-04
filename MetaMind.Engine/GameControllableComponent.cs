namespace MetaMind.Engine
{
    using System;
    using Microsoft.Xna.Framework;
    using Services;

    public abstract class GameControllableComponent : DrawableGameComponent, IGameControllableComponent
    {
        protected GameControllableComponent(GameEngine engine)
            : base(engine)
        {
            if (engine == null)
            {
                throw new ArgumentNullException(nameof(engine));
            }

            this.Engine = engine;

            this.Controllable = true;
        }

        public bool Controllable { get; private set; }

        public virtual void UpdateInput(GameTime time) { }

        #region Engine Data

        protected IGameInputService Input => GameEngine.Service.Input;

        protected IGameInteropService Interop => GameEngine.Service.Interop;

        protected IGameGraphicsService Graphics => GameEngine.Service.Graphics;

        protected IGameNumericalService Numerical => GameEngine.Service.Numerical;

        public GameEngine Engine { get; protected set; }

        #endregion Engine Data
    }
}