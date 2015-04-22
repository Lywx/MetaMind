namespace MetaMind.Perseverance.Concepts.Cognitions
{
    using System;
    using System.Runtime.Serialization;

    using MetaMind.Engine;

    using Microsoft.Xna.Framework;

    [DataContract]
    public class Consciousness : GameEntity, IConsciousness
    {
        #region Consciousness Data

        [DataMember]
        public readonly int AwakeHour = 7;

        [DataMember]
        public readonly int AwakeMinute = 0;

        [DataMember]
        public TimeSpan KnownAwakeSpan { get; set; }

        [DataMember]
        public TimeSpan KnownAsleepSpan { get; set; }

        [DataMember]
        public DateTime SleepEndTime { get; set; }

        [DataMember]
        public DateTime SleepStartTime { get; set; }

        [DataMember]
        private ConsciousnessState State { get; set; }

        #endregion Consciousness Data

        #region Consciousness Control

        /// <summary>
        ///     Awake when AwakeHour AM to 0 AM
        /// </summary>
        public bool HasAwaken
        {
            get
            {
                return DateTime.Now - DateTime.Today.AddHours(AwakeHour).AddMinutes(AwakeMinute) > TimeSpan.Zero;
            }
        }

        internal bool HasEverSlept
        {
            get
            {
                return this.SleepEndTime.Ticks != 0;
            }
        }

        public bool IsAwake
        {
            get
            {
                return this.State is ConsciousnessAwake;
            }
        }

        public bool IsAsleep
        {
            get
            {
                return this.State is ConsciousnessAsleep;
            }
        }

        #endregion

        #region Constructors and Destructors 

        public Consciousness()
        {
            this.SleepStartTime = DateTime.MinValue;
            this.SleepEndTime   = DateTime.MinValue;
            
            this.KnownAwakeSpan  = TimeSpan.Zero;
            this.KnownAsleepSpan = TimeSpan.Zero;

            this.State = new ConsciousnessAwake(this);
        }

        ~Consciousness()
        {
        }

        #endregion 


        #region Update

        public override void Update(GameTime time)
        {
            this.State.Update(time);
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