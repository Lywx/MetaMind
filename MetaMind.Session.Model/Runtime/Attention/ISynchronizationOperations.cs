namespace MetaMind.Session.Model.Runtime.Attention
{
    using Process;

    public interface ISynchronizationOperations
    {
        void ResetTomorrow();

        void ResetToday();

        /// <summary>
        /// Cancel possibly existing synchronization.
        /// </summary>
        void TryAbort();

        /// <summary>
        /// Cancel existing synchronization.
        /// </summary>
        void Abort();

        /// <summary>
        /// End existing synchronization.
        /// </summary>
        void Stop();

        void TryBegin(IJobSynchronizationData data);

        void Begin(IJobSynchronizationData data);
    }
}