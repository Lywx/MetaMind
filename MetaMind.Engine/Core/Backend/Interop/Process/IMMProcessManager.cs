namespace MetaMind.Engine.Core.Backend.Interop.Process
{
    using System;
    using Microsoft.Xna.Framework;

    public interface IMMProcessManager : IGameComponent, IDisposable
    {
        void AbortProcesses(bool immediate);

        void AttachProcess(IMMProcess process);
    }
}