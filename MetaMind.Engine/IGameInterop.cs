namespace MetaMind.Engine
{
    using MetaMind.Engine.Components;

    public interface IGameInterop
    {
        EventManager Event { get; }

        ProcessManager Process { get; }
    }
}