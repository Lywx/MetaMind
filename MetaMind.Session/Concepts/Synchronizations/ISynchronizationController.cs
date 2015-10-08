namespace MetaMind.Session.Concepts.Synchronizations
{
    public interface ISynchronizationController
    {
        void StartSynchronization();

        void StopSynchronization();

        void ToggleSynchronization();
    }
}