namespace MetaMind.Testimony.Concepts.Synchronizations
{
    public interface ISynchronizationOperations
    {
        void ResetTomorrow();

        void ResetToday();

        /// <summary>
        /// Cancel possibly existing synchronization
        /// </summary>
        void TryAbort();

        /// <summary>
        /// Cancel existing synchronization
        /// </summary>
        void Abort();

        /// <summary>
        /// End existing synchronization
        /// </summary>
        void Stop();

        void TryStart(ISynchronizationData data);

        void Start(ISynchronizationData data);
    }
}