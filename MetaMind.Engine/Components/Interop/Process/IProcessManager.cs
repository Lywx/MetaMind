namespace MetaMind.Engine.Components.Interop.Process
{
    using System;
    using Microsoft.Xna.Framework;

    public interface IProcessManager : IGameComponent, IDisposable
    {
        void AbortProcesses(bool immediate);

        void AttachProcess(IProcess process);
    }
}