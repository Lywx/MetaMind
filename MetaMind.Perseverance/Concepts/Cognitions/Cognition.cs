using System.Runtime.Serialization;

using MetaMind.Engine;

namespace MetaMind.Perseverance.Concepts.Cognitions
{
    [DataContract,
    KnownType(typeof(Consciousness)),
    KnownType(typeof(Synchronization))]
    public class Cognition : EngineObject
    {
        #region Components

        [DataMember]
        public IConsciousness Consciousness { get; set; }

        [DataMember]
        public ISynchronization Synchronization { get; set; }

        public bool Awake { get { return Consciousness.AwakeCondition; } }

        #endregion Components

        #region Constructors

        public Cognition()
        {
            Consciousness   = new ConsciousnessAwake();
            Synchronization = new Synchronization();
        }

        #endregion Constructors

        #region Update

        public void Update()
        {
            Consciousness = Consciousness.Update();
            Synchronization              .Update();
        }

        #endregion Update
    }
}