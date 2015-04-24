namespace MetaMind.Runtime.Concepts.Cognitions
{
    using System.Runtime.Serialization;

    using MetaMind.Runtime.Concepts.Synchronizations;

    public interface ICognition
    {
        [DataMember]
        IConsciousness Consciousness { get; set; }

        [DataMember]
        ISynchronization Synchronization { get; set; }

        void Update();
    }

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

        #region Update

        public void Update()
        {
            this.Consciousness  .Update();
            this.Synchronization.Update();
        }

        #endregion Update
    }
}