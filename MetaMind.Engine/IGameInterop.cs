namespace MetaMind.Engine
{
    using MetaMind.Engine.Components;

    public interface IGameInterop
    {
        IEventManager Event { get; }

        IGameManager Game { get; }

        GameEngine GameEngine { get; }

        IProcessManager Process { get; }

        IScreenManager Screen { get; }
    }
}