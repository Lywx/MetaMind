using MetaMind.Engine.Components.Events;
using MetaMind.Perseverance.Sessions;
using System;
using System.Runtime.Serialization;

namespace MetaMind.Perseverance.Concepts.Cognitions
{
    using MetaMind.Perseverance.Events;

    [DataContract]
    internal class ConsciousnessAsleep : ConsciousnessState
    {
        #region Constructors

        public ConsciousnessAsleep(Consciousness consciousness)
            : base(consciousness)
        {
        }

        #endregion Constructors

        #region Conversions

        public ConsciousnessAwake Awaken(Consciousness consciousness)
        {
            consciousness.SleepEndTime = DateTime.Now;
            var asleepSpan = consciousness.SleepEndTime - consciousness.SleepStartTime;
            consciousness.KnownAsleepSpan += asleepSpan;

            var @event = this.GameInterop.Event;
            @event.TriggerEvent(new Event((int)SessionEventType.SleepStopped, new ConsciousnessSleepStoppedEventArgs(this)));

            var console = this.GameInterop.Console;
            console.WriteLine(string.Format("MESSAGE: {0} in Sleep", asleepSpan.ToString("hh':'mm':'ss''")));

            return new ConsciousnessAwake(consciousness);
        }

        #endregion Conversions
    }
}