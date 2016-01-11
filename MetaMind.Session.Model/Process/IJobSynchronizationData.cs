namespace MetaMind.Session.Model.Process
{
    using Runtime.Attention;

    public interface IJobSynchronizationData
    {
        string Name { get; set; }

        bool IsSynchronizing { get; set; }

        SynchronizationSpan SynchronizationSpan { get; set; }
    }
}
