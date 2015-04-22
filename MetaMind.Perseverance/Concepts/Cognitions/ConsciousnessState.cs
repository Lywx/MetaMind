namespace MetaMind.Perseverance.Concepts.Cognitions
{
    using System.Runtime.Serialization;

    using MetaMind.Engine;

    [DataContract]
    internal class ConsciousnessState : GameEntity
    {
        protected ConsciousnessState(Consciousness consciousness)
        {
            this.Consciousness = consciousness;
        }

        protected Consciousness Consciousness { get; set; }
    }
}