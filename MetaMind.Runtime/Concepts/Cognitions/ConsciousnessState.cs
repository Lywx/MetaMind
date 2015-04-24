namespace MetaMind.Runtime.Concepts.Cognitions
{
    using System.Runtime.Serialization;

    using MetaMind.Engine;

    public interface IConsciousnessState
    {
        IConsciousnessState UpdateState(Consciousness consciousness);
    }

    [DataContract]
    internal abstract class ConsciousnessState : GameEntity, IConsciousnessState
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