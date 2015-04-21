namespace MetaMind.Next
{
    using MetaMind.Engine.Components.Inputs;

    class NullGameInput : IGameInput
    {
        public void Initialize()
        {
            
        }

        public IInputState State { get; private set; }

        public IInputEvent Event { get; private set; }
    }
}