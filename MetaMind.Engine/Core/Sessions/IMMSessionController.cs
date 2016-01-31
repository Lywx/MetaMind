namespace MetaMind.Engine.Core.Sessions
{
    using System;
    using Entity.Common;

    /// <summary>
    ///     Session controller is the main control for session management. It should
    ///     handle the communication with game engine and more advanced operations.
    /// </summary>
    public interface IMMSessionController<TData> : IMMFreeUpdatable,
        IMMLoadableOperations,
        IDisposable
    {
        TData Data { get; set; }
    }
}
