namespace MetaMind.Engine.Components.Inputs
{
    public interface IInputState : IGameControllableComponent
    {
        IKeyboardInputState Keyboard { get; }

        IMouseInputState Mouse { get; }
    }
}