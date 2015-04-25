namespace MetaMind.Runtime.Concepts.Tasks
{
    using System.Runtime.Serialization;

    using MetaMind.Runtime.Concepts.Progressions;
    using MetaMind.Runtime.Concepts.Synchronizations;

    [DataContract]
    public class Task : IProgressable, ISynchronizable
    {
        [DataMember]
        public string Name = string.Empty;

        public Task()
        {
            this.ProgressionData     = new ProgressionData();
            this.SynchronizationData = new SynchronizationData();
        }

        #region Progression Data

        [DataMember]
        public IProgressionData ProgressionData { get; set; }

        public string ProgressionName
        {
            get
            {
                return this.Name;
            }
        }

        #endregion

        #region Synchronization Data

        public string SynchronizationName
        {
            get
            {
                return this.Name;
            }
        }

        [DataMember]
        public ISynchronizationData SynchronizationData { get; set; }

        #endregion
    }
}