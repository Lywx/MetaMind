namespace MetaMind.Session.Model.Process
{
    using System;
    using Progress;
    using Runtime.Attention;

    /// <summary>
    /// A job is the abstraction of a task or a piece of work you have to do. 
    /// </summary>
    [DataContract]
    public class Job : MMEntity, IJob
    {
        public Job(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            this.Name = name;

            this.ProgressionData = new JobProgressionData(name);
            this.SynchronizationData = new JobSynchronizationData(name);
        }

        [DataMember]
        public string Name { get; set; }

        #region Progression Data

        [DataMember]
        public IJobProgressionData ProgressionData { get; set; }

        #endregion

        #region Synchronization Data

        [DataMember]
        public IJobSynchronizationData SynchronizationData { get; set; }

        #endregion

        public void ToggleSync()
        {
            if (!this.SynchronizationData.IsSynchronizing)
            {
                this.BeginSync();
            }
            else
            {
                this.EndSync();
            }
        }

        public void BeginSync()
        {
            this.Interop.Event.QueueEvent(
                new MMEvent(
                    (int)SessionEvent.SyncStarted,
                    new SynchronizationStartedEventArgs(
                        this.SynchronizationData)));
        }

        public void EndSync()
        {
            this.Interop.Event.QueueEvent(
                new MMEvent(
                    (int)SessionEvent.SyncStopped,
                    new SynchronizationStoppedEventArgs(
                        this.SynchronizationData)));
        }
    }
}