namespace MetaMind.Engine.Components
{
    using MetaMind.Engine.Components.Processes;

    using Microsoft.Xna.Framework;

    public interface IProcessManager : IGameComponent
    {
        void AbortProcesses(bool immediate);

        void AttachProcess(IProcess process);
    }
}