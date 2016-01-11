namespace MetaMind.Session.Model.Runtime.Attention
{
    using System;
    using Process;

    [DataContract]
    [KnownType(typeof(JobSynchronizationData))]
    public class SynchronizationProcessor
    {
        public SynchronizationProcessor()
        {
            
        }

        [DataMember]
        public IJobSynchronizationData Data { get; set; }

        public void Attach(IJobSynchronizationData data)
        {
            this.Data = data;

            this.Data.SynchronizationSpan += new SynchronizationSpan();
            this.Data.IsSynchronizing = true;
        }

        public void Detach(out TimeSpan timePassed)
        {
            timePassed = this.Data.SynchronizationSpan.End();

            this.Data.IsSynchronizing = false;
            this.Data = null;
        }

        public void Abort()
        {
            this.Data.SynchronizationSpan.Abort();

            this.Data.IsSynchronizing = false;
            this.Data = null;
        }

        public void Update(bool isEnabled, double acceleration)
        {
            if (isEnabled)
            {
                this.Data.SynchronizationSpan.Accelaration = acceleration;
            }
        }
    }
}
