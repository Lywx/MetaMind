namespace MetaMind.Session.Model.Runtime
{
    using System;

    [DataContract]
    [KnownType(typeof(ConsciousnessAsleep))]
    [KnownType(typeof(ConsciousnessAwake))]
    public class Consciousness : MMEntity, IConsciousness
    {
        #region Consciousness Data

        [DataMember]
        public readonly DateTime KnownOriginTime = DateTime.Now;

        [DataMember]
        public TimeSpan KnownAwakeDuration { get; set; }

        [DataMember]
        public TimeSpan KnownAsleepDuration { get; set; }

        [DataMember]
        public DateTime LastSleepBeginTime { get; set; }

        [DataMember]
        public DateTime LastSleepEndTime { get; set; }

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
            this.LastSleepBeginTime = DateTime.MinValue;
            this.LastSleepEndTime = DateTime.MinValue;

            this.KnownAwakeDuration = TimeSpan.Zero;
            this.KnownAsleepDuration = TimeSpan.Zero;

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