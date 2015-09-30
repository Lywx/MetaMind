namespace MetaMind.Engine.Components
{
    using Input;
    using Microsoft.Xna.Framework;

    internal class MMEngineNullInput : MMInputableComponent, IMMEngineInput
    {
        public IInputEvent Event { get; private set; }

        public IInputState State { get; private set; }

        public override void Initialize()
        {
        }

        public override void UpdateInput(GameTime time)
        {
        }

        public MMEngineNullInput(MMEngine engine) 
            : base(engine)
        {
        }
    }
}