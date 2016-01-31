namespace MetaMind.Engine.Core.Services
{
    using Backend.Input;

    public interface IMMEngineInputService
    {
        IMMInputEvent Event { get; }

        IMMInputState State { get; }
    }
}
