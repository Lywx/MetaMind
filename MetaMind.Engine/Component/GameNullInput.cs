namespace MetaMind.Engine.Component
{
    using Component.Input;
    using Microsoft.Xna.Framework;

    internal class GameNullInput : GameControllableComponent, IGameInput
    {
        public IInputEvent Event { get; private set; }

        public IInputState State { get; private set; }

        public override void Initialize()
        {
        }

        public override void UpdateInput(GameTime time)
        {
        }

        public GameNullInput(GameEngine engine) 
            : base(engine)
        {
        }
    }
}