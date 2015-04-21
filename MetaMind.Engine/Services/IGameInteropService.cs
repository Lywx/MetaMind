namespace MetaMind.Engine.Services
{
    using MetaMind.Engine.Components;

    public interface IGameInteropService
    {
        IEventManager Event { get; }

        IGameManager Game { get; }

        IGameEngine Engine { get; }

        IProcessManager Process { get; }

        IScreenManager Screen { get; }
    }
}