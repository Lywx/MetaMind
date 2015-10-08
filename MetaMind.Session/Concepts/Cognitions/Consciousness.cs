namespace MetaMind.Session.Concepts.Cognitions
{
    using System;
    using System.Runtime.Serialization;
    using Engine.Entities;

    [DataContract]
    [KnownType(typeof(ConsciousnessAsleep))]
    [KnownType(typeof(ConsciousnessAwake))]
    public class Consciousness : MMEntity, IConsciousness
    {
        #region Consciousness Data

        [DataMember]
        public readonly DateTime KnownOriginTime = DateTime.Now;

        [DataMember]
        public TimeSpan KnownAwakeSpan { get; set; }

        [DataMember]
        public TimeSpan KnownAsleepSpan { get; set; }

        [DataMember]
        public DateTime LastSleepEndTime { get; set; }

        [DataMember]
        public DateTime LastSleepStartTime { get; set; }

        [DataMember]
        public IConsciousnessState State { get; set; }

        #endregion Consciousness Data

        #region Consciousness Control

        internal bool HasSlept => this.LastSleepEndTime.Ticks != 0;

        public bool IsAwake => this.State is ConsciousnessAwake;

        public bool IsAsleep => this.State is ConsciousnessAsleep;

        #endregion

        #region Constructors and Finalizer 

        public Consciousness()
        {
            this.LastSleepStartTime = DateTime.MinValue;
            this.LastSleepEndTime   = DateTime.MinValue;
            
            this.KnownAwakeSpan  = TimeSpan.Zero;
            this.KnownAsleepSpan = TimeSpan.Zero;

            this.State = new ConsciousnessAwake(this);
        }

        #endregion 

        #region Update

        public void Update()
        {
            this.State = this.State.UpdateState(this);
        }

        #endregion Update
        
        #region Operations

        public void Sleep()
        {
            var awake = this.State as ConsciousnessAwake;
            if (awake != null)
            {
                this.State = awake.Sleep(this);
            }
        }

        public void Awaken()
        {
            var asleep = this.State as ConsciousnessAsleep;
            if (asleep != null)
            {
                this.State = asleep.Awaken(this);
            }
        }

        #endregion

    }
}