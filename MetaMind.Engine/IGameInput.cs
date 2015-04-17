namespace MetaMind.Engine
{
    using MetaMind.Engine.Components;
    using MetaMind.Engine.Components.Inputs;

    public interface IGameInput
    {
        IInputEvent Event { get; }

        IInputState State { get; }
    }
}