namespace MetaMind.Engine
{
    using Microsoft.Xna.Framework;

    public abstract class GameControllableComponent : DrawableGameComponent, IGameControllableComponent
    {
        public GameControllableComponent(Microsoft.Xna.Framework.Game engine) : base(engine)
        {
            this.Controllable = true;
        }

        public abstract void UpdateInput(GameTime gameTime);

        public bool Controllable { get; private set; }
    }
}