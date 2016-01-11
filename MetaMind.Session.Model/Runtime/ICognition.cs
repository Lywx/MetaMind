namespace MetaMind.Session.Model.Runtime
{
    using Attention;

    public interface ICognition: IMMFreeUpdatable
    {
        [DataMember]
        IConsciousness Consciousness { get; set; }

        float SynchronizationRate { get; }

        [DataMember]
        ISynchronizationData SynchronizationData { get; set; }
    }
}