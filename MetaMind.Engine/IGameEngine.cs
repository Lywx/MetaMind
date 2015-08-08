namespace MetaMind.Engine
{
    using Components;

    public interface IGameEngine : IGameEngineOperations
    {
        IGameInput Input { get; }

        IGameInterop Interop { get; }

        IGameGraphics Graphics { get; }
    }
}