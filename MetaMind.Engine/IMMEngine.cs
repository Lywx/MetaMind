namespace MetaMind.Engine
{
    using System;
    using Components;

    public interface IMMEngine : IDisposable, IMMEngineOperations
    {
        IMMEngineInput Input { get; }

        IMMEngineInterop Interop { get; }

        IMMEngineGraphics Graphics { get; }
    }
}