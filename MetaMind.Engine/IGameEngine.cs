namespace MetaMind.Engine
{
    using System;
    using Components;

    public interface IGameEngine : IDisposable, IGameEngineOperations
    {
        IGameInput Input { get; }

        IGameInterop Interop { get; }

        IGameGraphics Graphics { get; }
    }
}