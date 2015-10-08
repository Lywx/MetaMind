namespace MetaMind.Engine.Sessions
{
    using System;
    using Entities;

    /// <summary>
    ///     Session controller is the main control for session management. It should
    ///     handle the communication with game engine and more advanced operations.
    /// </summary>
    public interface IMMSessionController<TData> : IMMFreeUpdatable,
        IMMInteropOperations,
        IDisposable
    {
        TData Data { get; set; }
    }
}
