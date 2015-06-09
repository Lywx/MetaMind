namespace MetaMind.Engine
{
    using MetaMind.Engine.Components;

    public interface IGameEngine : IGameEngineOperations
    {
        IGameInput Input { get; }

        IGameInterop Interop { get; }

        IGameGraphics Graphics { get; }
    }
}