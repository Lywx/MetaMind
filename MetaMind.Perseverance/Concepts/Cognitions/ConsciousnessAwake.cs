using MetaMind.Engine.Components.Events;
using MetaMind.Perseverance.Sessions;
using System;
using System.Runtime.Serialization;

namespace MetaMind.Perseverance.Concepts.Cognitions
{
    [DataContract]
    public class ConsciousnessAwake : Consciousness
    {
        public ConsciousnessAwake()
        {
        }

        public ConsciousnessAwake(ConsciousnessSleepy state)
        {
            this.SleepEndTime        = state.SleepEndTime;
            this.SleepStartTime      = state.SleepStartTime;
            this.HistoricalAwakeSpan = state.HistoricalAwakeSpan;
            this.HistoricalSleepSpan = state.HistoricalSleepSpan;
        }

        public TimeSpan AwakeSpan
        {
            get { return DateTime.Now - this.SleepEndTime; }
        }

        public ConsciousnessSleepy StartSleeping()
        {
            SleepStartTime = DateTime.Now;

            if (HasEverSlept)
            {
                var totalAwakeSpan = this.SleepStartTime - this.SleepEndTime;
                this.HistoricalAwakeSpan += totalAwakeSpan;

                MessageManager.PopMessages("Awake for " + totalAwakeSpan.ToString("hh':'mm':'ss''"));
            }

            // add to event queue
            var sleepStartedEvent = new EventBase(
                (int)SessionEventType.SleepStarted,
                new ConsciousnessSleepStartedEventArgs(this));
            EventManager.TriggerEvent(sleepStartedEvent);

            return new ConsciousnessSleepy(this);
        }
    }
}