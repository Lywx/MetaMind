namespace MetaMind.Unity.Concepts.Cognitions
{
    using System.Runtime.Serialization;
    using Engine;
    using Engine.Entities;

    public interface IConsciousnessState
    {
        IConsciousnessState UpdateState(Consciousness consciousness);
    }

    [DataContract]
    internal abstract class ConsciousnessState : MMEntity, IConsciousnessState
    {
        protected ConsciousnessState(Consciousness consciousness)
        {
            this.Consciousness = consciousness;
        }

        [DataMember]
        protected Consciousness Consciousness { get; set; }

        public abstract IConsciousnessState UpdateState(Consciousness consciousness);
    }
}