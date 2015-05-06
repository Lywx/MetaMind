namespace MetaMind.Testimony.Concepts.Motivations
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    using MetaMind.Testimony.Concepts.Progressions;
    using MetaMind.Testimony.Concepts.Tasks;

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