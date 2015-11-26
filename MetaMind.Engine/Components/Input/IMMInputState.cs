namespace MetaMind.Engine.Components.Input
{
    public interface IMMInputState : IMMInputableComponent
    {
        IMMKeyboardInput Keyboard { get; }

        IMMMouseInput Mouse { get; }
    }
}