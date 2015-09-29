namespace MetaMind.Engine.Components.Input
{
    public interface IInputState : IGameInputableComponent
    {
        IKeyboardInputState Keyboard { get; }

        IMouseInputState Mouse { get; }
    }
}