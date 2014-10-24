using System;
using System.Runtime.Serialization;
using MetaMind.Engine.Components.Events;
using MetaMind.Perseverance.Sessions;

namespace MetaMind.Perseverance.Concepts.Cognitions
{
    [DataContract]
    public class ConsciousnessSleepy : Consciousness
    {
        [DataMember]
        public TimeSpan SleepSpan;

        #region Constructors

        public ConsciousnessSleepy( ConsciousnessAwake state )
        {
            SleepEndTime = state.SleepEndTime;
            SleepStartTime = state.SleepStartTime;
            HistoricalAwakeSpan = state.HistoricalAwakeSpan;
            HistoricalSleepSpan = state.HistoricalSleepSpan;
        }

        #endregion Constructors

        #region Conversions

        public ConsciousnessAwake StopSleeping()
        {
            SleepEndTime = DateTime.Now;
            SleepSpan = SleepEndTime - SleepStartTime;
            HistoricalSleepSpan += SleepSpan;

            // add to event queue
            var sleepStoppedEvent = new EventBase( ( int )
                AdventureEventType.SleepStopped,
                new ConsciousnessSleepStoppedEventArgs( this ) );
            EventManager.TriggerEvent( sleepStoppedEvent );

            MessageManager.PopMessages( "Slept for " + SleepSpan.ToString( "hh':'mm':'ss''" ) );

            return new ConsciousnessAwake( this );
        }

        #endregion Conversions
    }
}