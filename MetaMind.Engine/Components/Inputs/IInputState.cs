namespace MetaMind.Engine.Components.Inputs
{
    public interface IInputState : IGameInputableComponent
    {
        IKeyboardInputState Keyboard { get; }

        IMouseInputState Mouse { get; }
    }
}