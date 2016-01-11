namespace MetaMind.Session.Model.Process.Progress
{
    public interface IJobProgressionData
    {
        [DataMember]
        string Name { get; set; }

        [DataMember]
        float Total { get; set; }

        [DataMember]
        float Done { get; set; }
    }
}