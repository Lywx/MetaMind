namespace MetaMind.Engine.Components.Input
{
    using Mouse;

    public interface IMMInputState : IMMInputableComponent
    {
        IMMKeyboardInput Keyboard { get; }

        IMMMouseInput Mouse { get; }
    }
}