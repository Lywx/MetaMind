namespace MetaMind.PerseveranceService.Services
{
    using System.ServiceModel;

    using MetaMind.Perseverance;
    using MetaMind.Perseverance.Concepts.TaskEntries;

    [ServiceContract]
    public interface ISynchronizationService
    {
        [OperationContract]
        TaskEntry GetSynchronizedTask();
    }

    public class SynchronizationService : ISynchronizationService
    {
        public TaskEntry GetSynchronizedTask()
        {
            return Perseverance.Adventure.Cognition.Synchronization.SynchronizedTask;
        }
    }
}
