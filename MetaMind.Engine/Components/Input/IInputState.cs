namespace MetaMind.Engine.Components.Input
{
    public interface IInputState : IGameControllableComponent
    {
        IKeyboardInputState Keyboard { get; }

        IMouseInputState Mouse { get; }
    }
}