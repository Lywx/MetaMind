namespace MetaMind.Session.Model.Process
{
    using System;
    using Runtime.Attention;

    [DataContract]
    public class JobSynchronizationData : IJobSynchronizationData
    {
        public JobSynchronizationData(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            this.Name = name;
        }

        public string Name { get; set; }

        [DataMember]
        public bool IsSynchronizing { get; set; } = false;

        [DataMember]
        public SynchronizationSpan SynchronizationSpan { get; set; } = SynchronizationSpan.Zero;
    }
}