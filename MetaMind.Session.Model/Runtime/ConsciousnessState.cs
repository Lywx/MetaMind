namespace MetaMind.Session.Model.Runtime
{
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