namespace MetaMind.PerseveranceService.Services
{
    using System.ServiceModel;

    using MetaMind.Perseverance;
    using MetaMind.Perseverance.Concepts;
    using MetaMind.Perseverance.Concepts.Tasks;

    [ServiceContract]
    public interface ISynchronizationService
    {
        [OperationContract]
        [ServiceKnownType(typeof(Synchronization))]
        ISynchronization FetchSynchronization();

        [OperationContract]
        [ServiceKnownType(typeof(Task))]
        ISynchronizable FetchSynchronizationData();
    }

    public class SynchronizationService : ISynchronizationService
    {
        public ISynchronization FetchSynchronization()
        {
            return Perseverance.Session.Cognition.Synchronization;
        }

        public ISynchronizable FetchSynchronizationData()
        {
            return Perseverance.Session.Cognition.Synchronization.SynchronizedData;
        }
    }
}
