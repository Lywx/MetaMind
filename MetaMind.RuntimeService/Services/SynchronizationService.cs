namespace MetaMind.RuntimeService.Services
{
    using System.ServiceModel;

    using MetaMind.Runtime;
    using MetaMind.Runtime.Concepts.Tasks;

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
            return Runtime.Session.Cognition.Synchronization;
        }

        public ISynchronizable FetchSynchronizationData()
        {
            return Runtime.Session.Cognition.Synchronization.SynchronizedData;
        }
    }
}
