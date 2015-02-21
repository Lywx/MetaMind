namespace MetaMind.Perseverance.Concepts.Cognitions
{
    using System;
    using System.Runtime.Serialization;

    using MetaMind.Engine;

    public interface IConsciousness
    {
        bool AwakeCondition { get; }

        Consciousness Update();
    }

    [DataContract,
     KnownType(typeof(ConsciousnessAwake)),
     KnownType(typeof(ConsciousnessSleepy))]
    public class Consciousness : EngineObject, IConsciousness
    {
        #region Consciousness Data

        [DataMember]
        public static int AwakeHour = 7;

        [DataMember]
        public static int AwakeMinute = 0;

        [DataMember]
        public TimeSpan HistoricalAwakeSpan { get; set; }

        [DataMember]
        public TimeSpan HistoricalSleepSpan { get; set; }

        [DataMember]
        public DateTime SleepEndTime { get; set; }

        [DataMember]
        public DateTime SleepStartTime { get; set; }

        #endregion Consciousness Data

        #region Consciousness Control

        /// <summary>
        ///     Awake when 0 AM - awaken condition(7 AM for example) - 0 AM
        /// </summary>
        public bool AwakeCondition
        {
            get
            {
                return DateTime.Now - DateTime.Today.AddHours(AwakeHour).AddMinutes(AwakeMinute) > TimeSpan.Zero;
            }
        }

        protected bool HasEverSlept
        {
            get { return SleepEndTime.Ticks != 0; }
        }

        #endregion Consciousness Control

        #region Constructors

        public Consciousness()
        {
            SleepEndTime        = DateTime.MinValue;
            SleepStartTime      = DateTime.MinValue;
            HistoricalAwakeSpan = TimeSpan.Zero;
            HistoricalSleepSpan = TimeSpan.Zero;
        }

        #endregion Constructors

        #region Update

        public Consciousness Update()
        {
            if (!AwakeCondition &&
                this is ConsciousnessAwake)
            {
                return ((ConsciousnessAwake)this).StartSleeping();
            }

            if (AwakeCondition &&
                this is ConsciousnessSleepy)
            {
                return ((ConsciousnessSleepy)this).StopSleeping();
            }

            return this;
        }

        #endregion Update
    }
}