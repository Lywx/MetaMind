namespace MetaMind.Runtime.Concepts.Motivations
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    using MetaMind.Runtime.Concepts.Progressions;
    using MetaMind.Runtime.Concepts.Tasks;

    [DataContract]
    public class Motivation : IProgressable
    {
        [DataMember]
        public string Name = string.Empty;

        [DataMember]
        public List<Task> Tasks = new List<Task>();

        public Motivation()
        {
            this.ProgressionData = new ProgressionData();
        }

        #region Progression Data

        public IProgressionData ProgressionData { get; set; }

        public string ProgressionName
        {
            get { return this.Name; }
        }

        #endregion
    }
}