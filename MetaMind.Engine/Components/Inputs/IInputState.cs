namespace MetaMind.Engine.Components.Inputs
{
    public interface IInputState
    {
        IKeyboardInputState Keyboard { get; }

        IMouseInputState Mouse { get; }
    }
}