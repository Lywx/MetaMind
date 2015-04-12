namespace MetaMind.Engine
{
    using MetaMind.Engine.Components;

    public interface IGameInterop
    {
        EventManager Event { get; }

        ProcessManager Process { get; }

        GameManager Game { get; }

        GameEngine GameEngine { get; }
    }
}