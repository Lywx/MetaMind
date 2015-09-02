namespace MetaMind.Engine
{
    using System;
    using Services;

    public class GameComponent : Microsoft.Xna.Framework.GameComponent
    {
        protected GameComponent(GameEngine engine) : base(engine)
        {
            if (engine == null)
            {
                throw new ArgumentNullException(nameof(engine));
            }

            this.Engine = engine;
        }

        protected GameEngine Engine { get; private set; }

        protected IGameInteropService GameInterop => GameEngine.Service.Interop;

        protected IGameGraphicsService GameGraphics => GameEngine.Service.Graphics;

        protected IGameNumericalService GameNumerical => GameEngine.Service.Numerical;
    }
}
