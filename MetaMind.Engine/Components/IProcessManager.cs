namespace MetaMind.Engine.Components
{
    using MetaMind.Engine.Components.Processes;

    public interface IProcessManager
    {
        void AbortProcesses(bool immediate);

        void AttachProcess(IProcess process);
    }
}