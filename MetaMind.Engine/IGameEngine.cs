namespace MetaMind.Engine
{
    using System;
    using Component;

    public interface IGameEngine : IDisposable, IGameEngineOperations
    {
        IGameInput Input { get; }

        IGameInterop Interop { get; }

        IGameGraphics Graphics { get; }
    }
}