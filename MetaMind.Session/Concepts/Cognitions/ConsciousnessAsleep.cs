namespace MetaMind.Session.Concepts.Cognitions
{
    using System;
    using System.Runtime.Serialization;
    using Engine.Components.Interop.Event;
    using Session.Sessions;

    [DataContract]
    internal class ConsciousnessAsleep : ConsciousnessState, IConsciousnessAsleep
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
            consciousness.LastSleepEndTime = DateTime.Now;

            var asleepSpan = consciousness.LastSleepEndTime - consciousness.LastSleepStartTime;
            consciousness.KnownAsleepSpan += asleepSpan;

            var @event = this.Interop.Event;
            @event.TriggerEvent(new MMEvent((int)SessionEvent.SleepStopped, new ConsciousnessSleepStoppedEventArgs(consciousness)));

            var console = this.Interop.Console;
            console.WriteLine($"MESSAGE: Asleep {asleepSpan.ToString("hh':'mm':'ss''")}");

            return new ConsciousnessAwake(consciousness);
        }

        #endregion Conversions

        public override IConsciousnessState UpdateState(Consciousness consciousness)
        {
            return this;
        }
    }
}