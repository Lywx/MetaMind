namespace MetaMind.Engine.Services
{
    using Components.Input;

    public interface IMMEngineInputService
    {
        IMMInputEvent Event { get; }

        IMMInputState State { get; }
    }
}
