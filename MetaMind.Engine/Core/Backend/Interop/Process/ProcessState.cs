namespace MetaMind.Engine.Core.Backend.Interop.Process
{
    public enum ProcessState
    {
        // Processes that are neither dead or alive
        Uninitilized,

        Removed,

        // Living Processes
        Running,

        Paused,

        // Dead Processes
        Succeeded,

        Failed,

        Aborted
    }
}
