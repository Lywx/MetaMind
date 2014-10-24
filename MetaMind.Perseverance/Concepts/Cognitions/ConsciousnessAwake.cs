using System;
using System.Runtime.Serialization;
using MetaMind.Engine.Components.Events;
using MetaMind.Perseverance.Sessions;

namespace MetaMind.Perseverance.Concepts.Cognitions
{
    [DataContract]
    public class ConsciousnessAwake : Consciousness
    {
        [DataMember]
        public TimeSpan AwakeSpan { get; set; }

        #region Constructors

        public ConsciousnessAwake()
        {
        }

        public ConsciousnessAwake( ConsciousnessSleepy state )
        {
            SleepEndTime = state.SleepEndTime;
            SleepStartTime = state.SleepStartTime;
            HistoricalAwakeSpan = state.HistoricalAwakeSpan;
            HistoricalSleepSpan = state.HistoricalSleepSpan;
        }

        #endregion Constructors

        #region Conversion

        public ConsciousnessSleepy StartSleeping()
        {
            SleepStartTime = DateTime.Now;
            if ( HasEverSlept )
            {
                AwakeSpan = SleepStartTime - SleepEndTime;
                HistoricalAwakeSpan += AwakeSpan;
            }
            // add to event queue
            var sleepStartedEvent = new EventBase( ( int )
                AdventureEventType.SleepStarted,
                new ConsciousnessSleepStartedEventArgs( this ) );
            EventManager.TriggerEvent( sleepStartedEvent );
            if ( HasEverSlept )
            {
                MessageManager.PopMessages( "Awake for " + AwakeSpan.ToString( "hh':'mm':'ss''" ) );
            }
            return new ConsciousnessSleepy( this );
        }

        #endregion Conversion
    }
}