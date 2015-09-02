namespace MetaMind.Engine
{
    using Microsoft.Xna.Framework;

    public abstract class GameControllableComponent : DrawableGameComponent, IGameControllableComponent
    {
        protected GameControllableComponent(GameEngine engine)
            : base(engine)
        {
            this.Controllable = true;
        }

        public bool Controllable { get; private set; }

        public virtual void UpdateInput(GameTime time) { }
    }
}