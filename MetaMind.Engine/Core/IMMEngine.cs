namespace MetaMind.Engine.Core
{
    using System;
    using Backend;

    public interface IMMEngine : IDisposable, IMMEngineOperations
    {
        IMMEngineInput Input { get; }

        IMMEngineInterop Interop { get; }

        IMMEngineGraphics Graphics { get; }
    }
}