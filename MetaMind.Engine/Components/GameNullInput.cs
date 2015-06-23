namespace MetaMind.Engine.Components
{
    using MetaMind.Engine.Components.Inputs;

    using Microsoft.Xna.Framework;

    internal class GameNullInput : GameControllableComponent, IGameInput
    {
        public IInputEvent Event { get; private set; }

        public IInputState State { get; private set; }

        public override void Initialize()
        {
        }

        public override void UpdateInput(GameTime gameTime)
        {
        }

        public GameNullInput(GameEngine engine) 
            : base(engine)
        {
        }
    }
}