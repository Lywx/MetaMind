namespace MetaMind.Engine.Core.Backend
{
    using Input;
    using Microsoft.Xna.Framework;

    internal class MMEngineNullInput : ImmInputableComponent, IMMEngineInput
    {
        public IMMInputEvent Event { get; private set; }

        public IMMInputState State { get; private set; }

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