namespace MetaMind.Engine.Core.Backend.Input
{
    using Keyboard;
    using Mouse;

    public interface IMMInputState : IMMGeneralComponent 
    {
        IMMKeyboardInput Keyboard { get; }

        IMMMouseInput Mouse { get; }
    }
}