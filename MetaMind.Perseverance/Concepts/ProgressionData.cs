namespace MetaMind.Perseverance.Concepts
{
    using System.Runtime.Serialization;

    public interface IProgressable
    {
        ProgressionData ProgressionData { get; set; }

        string ProgressionName { get; }
    }

    public interface IProgressionData
    {
        [DataMember]
        int Done { get; set; }

        [DataMember]
        int Load { get; set; }
    }

    [DataContract]
    public class ProgressionData : IProgressionData
    {
        public ProgressionData()
        {
            this.Load = 0;
            this.Done = 0;
        }

        [DataMember]
        public int Done { get; set; }

        [DataMember]
        public int Load { get; set; }
    }
}