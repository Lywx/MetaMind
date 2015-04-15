namespace MetaMind.Engine
{
    using MetaMind.Engine.Components;

    public interface IGameInput
    {
        IInputEvent Event { get; }

        IInputState State { get; }
    }
}