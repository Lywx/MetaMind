namespace MetaMind.Engine.Component.Input
{
    public interface IInputState : IGameControllableComponent
    {
        IKeyboardInputState Keyboard { get; }

        IMouseInputState Mouse { get; }
    }
}