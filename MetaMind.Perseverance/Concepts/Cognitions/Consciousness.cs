using MetaMind.Engine;
using Microsoft.Xna.Framework;
using System;
using System.Runtime.Serialization;

namespace MetaMind.Perseverance.Concepts.Cognitions
{
    public interface IConsciousness
    {
        Consciousness Update(GameTime gameTime);

        bool AwakeCondition { get; }
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
        public static int AwakeMinute = 59;

        [DataMember]
        public DateTime SleepEndTime { get; set; }

        [DataMember]
        public DateTime SleepStartTime { get; set; }

        [DataMember]
        public TimeSpan HistoricalAwakeSpan { get; set; }

        [DataMember]
        public TimeSpan HistoricalSleepSpan { get; set; }

        #endregion Consciousness Data

        #region Consciousness Control

        public bool HasEverSlept
        {
            get { return SleepEndTime.Ticks != 0; }
        }

        public bool AwakeCondition
        {
            get
            {
                var now = DateTime.Now;
                return now - DateTime.Today.AddHours(AwakeHour).AddMinutes(AwakeMinute) > TimeSpan.Zero;
            }
        }

        #endregion Consciousness Control

        #region Constructors

        public Consciousness()
        {
            SleepEndTime = DateTime.MinValue;
            SleepStartTime = DateTime.MinValue;
            HistoricalAwakeSpan = TimeSpan.Zero;
            HistoricalSleepSpan = TimeSpan.Zero;
        }

        #endregion Constructors

        #region Update

        public Consciousness Update(GameTime gameTime)
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