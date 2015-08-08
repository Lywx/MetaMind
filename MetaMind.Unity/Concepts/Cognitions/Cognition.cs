namespace MetaMind.Unity.Concepts.Cognitions
{
    using System.Runtime.Serialization;
    using Synchronizations;

    [DataContract]
    [KnownType(typeof(Consciousness))]
    [KnownType(typeof(Synchronization))]
    public class Cognition : ICognition
    {
        #region Components

        [DataMember]
        public IConsciousness Consciousness { get; set; }

        [DataMember]
        public ISynchronization Synchronization { get; set; }

        #endregion Components

        #region Constructors

        public Cognition()
        {
            this.Consciousness   = new Consciousness();
            this.Synchronization = new Synchronization();
        }

        #endregion Constructors

        public int SynchronizationRatio
        {
            get
            {
                return this.Consciousness.IsAwake
                    ? (int)
                        (this.Synchronization.SynchronizedTimeTodayBestCase.TotalSeconds
                         / ((IConsciousnessAwake)this.Consciousness.State).AwakeSpan.TotalSeconds * 100)
                    : 0;
            } 
        }

        #region Update

        public void Update()
        {
            this.Consciousness  .Update();
            this.Synchronization.Update();
        }

        #endregion Update
    }
}