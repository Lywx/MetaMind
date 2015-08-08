namespace MetaMind.Unity.Concepts.Synchronizations
{
    public interface ISynchronizationController
    {
        void StartSynchronization();

        void StopSynchronization();

        void ToggleSynchronization();
    }
}