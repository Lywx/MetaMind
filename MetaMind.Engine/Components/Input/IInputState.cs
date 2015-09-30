namespace MetaMind.Engine.Components.Input
{
    public interface IInputState : IMMInputableComponent
    {
        IKeyboardInputState Keyboard { get; }

        IMouseInputState Mouse { get; }
    }
}