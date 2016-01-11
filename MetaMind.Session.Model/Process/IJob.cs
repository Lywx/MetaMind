namespace MetaMind.Session.Model.Process
{
    using Progress;

    public interface IJob
    {
        string Name { get; set; }

        IJobSynchronizationData SynchronizationData { get; set; }

        IJobProgressionData ProgressionData { get; set; }
    }
}