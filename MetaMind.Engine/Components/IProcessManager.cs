namespace MetaMind.Engine.Components
{
    using System;
    using Processes;

    using Microsoft.Xna.Framework;

    public interface IProcessManager : IGameComponent, IDisposable
    {
        void AbortProcesses(bool immediate);

        void AttachProcess(IProcess process);
    }
}