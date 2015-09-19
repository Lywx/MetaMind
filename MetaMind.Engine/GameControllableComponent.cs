namespace MetaMind.Engine
{
    using System;
    using Microsoft.Xna.Framework;
    using Service;

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

        #region Engine Service Data

        protected IGameInputService Input => GameEngine.Service.Input;

        protected IGameInteropService Interop => GameEngine.Service.Interop;

        protected IGameGraphicsService Graphics => GameEngine.Service.Graphics;

        protected IGameNumericalService Numerical => GameEngine.Service.Numerical;

        public GameEngine Engine { get; protected set; }

        #endregion

        #region Draw

        public virtual void BeginDraw(GameTime time)
        {
        }

        public virtual void EndDraw(GameTime time)
        {
        }

        #endregion

        #region Update

        public virtual void UpdateInput(GameTime time)
        {
        }

        #endregion
    }
}