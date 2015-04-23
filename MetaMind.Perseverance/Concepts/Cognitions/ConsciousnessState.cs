namespace MetaMind.Perseverance.Concepts.Cognitions
{
    using System.Runtime.Serialization;

    using MetaMind.Engine;

    [DataContract]
    internal abstract class ConsciousnessState : GameEntity
    {
        protected ConsciousnessState(Consciousness consciousness)
        {
            this.Consciousness = consciousness;
        }

        [DataMember]
        protected Consciousness Consciousness { get; set; }

        public abstract ConsciousnessState UpdateState(Consciousness consciousness);
    }
}