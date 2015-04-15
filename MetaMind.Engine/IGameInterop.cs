namespace MetaMind.Engine
{
    using MetaMind.Engine.Components;

    public interface IGameInterop
    {
        IEventManager Events { get; }

        IGameManager Game { get; }

        GameEngine GameEngine { get; }

        IProcessManager Processes { get; }

        IScreenManager Screens { get; }
    }
}