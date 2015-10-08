namespace MetaMind.Session.Concepts.Synchronizations
{
    public interface ISynchronizable
    {
        string SynchronizationName { get; }

        ISynchronizationData SynchronizationData { get; set; }
    }
}