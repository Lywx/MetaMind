namespace MetaMind.Testimony.Concepts.Synchronizations
{
    public interface ISynchronizable
    {
        string SynchronizationName { get; }

        ISynchronizationData SynchronizationData { get; set; }
    }
}