namespace MetaMind.Unity.Concepts.Cognitions
{
    using System.Runtime.Serialization;
    using Engine;
    using Synchronizations;

    public interface ICognition: IMMFreeUpdatable, ICognitionProperties
    {
        [DataMember]
        IConsciousness Consciousness { get; set; }

        [DataMember]
        ISynchronization Synchronization { get; set; }
    }
}