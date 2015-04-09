namespace MetaMind.Perseverance.Concepts.Tasks
{
    using System.Runtime.Serialization;

    using MetaMind.Engine;

    [DataContract]
    public class Task : GameEngineAccess, IProgressable, ISynchronizable
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
        public ProgressionData ProgressionData { get; set; }

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
        public SynchronizationData SynchronizationData { get; set; }

        #endregion
    }
}