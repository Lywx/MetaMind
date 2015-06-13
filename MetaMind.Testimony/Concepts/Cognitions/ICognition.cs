namespace MetaMind.Testimony.Concepts.Cognitions
{
    using System.Runtime.Serialization;
    using Synchronizations;

    public interface ICognition: IInnerUpdatable, ICognitionProperties
    {
        [DataMember]
        IConsciousness Consciousness { get; set; }

        [DataMember]
        ISynchronization Synchronization { get; set; }
    }
}