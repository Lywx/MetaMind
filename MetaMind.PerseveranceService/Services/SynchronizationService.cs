namespace MetaMind.PerseveranceService.Services
{
    using System.ServiceModel;

    using MetaMind.Perseverance;
    using MetaMind.Perseverance.Concepts.Cognitions;
    using MetaMind.Perseverance.Concepts.TaskEntries;

    [ServiceContract]
    public interface ISynchronizationService
    {
        [OperationContract]
        [ServiceKnownType(typeof(Synchronization))]
        ISynchronization Fetch();

        [OperationContract]
        TaskEntry FetchTask();
    }

    public class SynchronizationService : ISynchronizationService
    {
        public ISynchronization Fetch()
        {
            return Perseverance.Adventure.Cognition.Synchronization;
        }

        public TaskEntry FetchTask()
        {
            return Perseverance.Adventure.Cognition.Synchronization.SynchronizedTask;
        }
    }
}
