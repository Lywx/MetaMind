namespace MetaMind.Engine.Components
{
    using MetaMind.Engine.Components.Inputs;

    using Microsoft.Xna.Framework;

    internal class NullGameInput : IGameInput
    {
        public IInputEvent Event { get; private set; }

        public IInputState State { get; private set; }

        public void Initialize()
        {
        }

        public void UpdateInput(GameTime gameTime)
        {
        }
    }
}