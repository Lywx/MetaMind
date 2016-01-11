namespace MetaMind.Session.Model.Process.Progress
{
    using System;

    [DataContract]
    public class JobProgressionData : IJobProgressionData
    {
        public JobProgressionData(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            this.Name = name;
        }

        public JobProgressionData(string name, float done, float total) : this(name)
        {
            this.Done  = done;
            this.Total = total;
        }

        public string Name { get; set; }

        [DataMember]
        public float Total { get; set; } = 0;

        [DataMember]
        public float Done { get; set; } = 0;
    }
}
