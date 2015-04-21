﻿namespace MetaMind.Perseverance.Concepts.Cognitions
{
    using System.Runtime.Serialization;

    public interface ICognition
    {
        [DataMember]
        IConsciousness Consciousness { get; set; }

        [DataMember]
        ISynchronization Synchronization { get; set; }

        bool Awake { get; }

        void Update();
    }

    [DataContract,
    KnownType(typeof(Consciousness)),
    KnownType(typeof(Synchronization))]
    public class Cognition : ICognition
    {
        #region Components

        [DataMember]
        public IConsciousness Consciousness { get; set; }

        [DataMember]
        public ISynchronization Synchronization { get; set; }

        public bool Awake
        {
            get
            {
                return this.Consciousness.AwakeCondition;
            }
        }

        #endregion Components

        #region Constructors

        public Cognition()
        {
            this.Consciousness   = new ConsciousnessAwake();
            this.Synchronization = new Synchronization();
        }

        #endregion Constructors

        #region Update

        public void Update()
        {
            this.Consciousness = this.Consciousness.Update();
            this.Synchronization.Update();
        }

        #endregion Update
    }
}