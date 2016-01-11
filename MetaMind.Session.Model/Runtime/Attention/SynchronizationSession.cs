namespace MetaMind.Session.Model.Runtime.Attention
{
    using Process;

    public class SynchronizationSession : MMEntity, ISynchronizationController
    {
        public SynchronizationSession()
        {
            this.JobSynchronizationData = new JobSynchronizationData();
        }

        protected IJobSynchronizationData JobSynchronizationData { get; private set; }
    }
}