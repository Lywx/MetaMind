namespace MetaMind.Runtime.Concepts.Cognitions
{
    using System;
    using System.Runtime.Serialization;

    using MetaMind.Engine.Components.Events;
    using MetaMind.Runtime.Events;
    using MetaMind.Runtime.Sessions;

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

            var @event = this.GameInterop.Event;
            @event.TriggerEvent(new Event((int)SessionEventType.SleepStopped, new ConsciousnessSleepStoppedEventArgs(consciousness)));

            var console = this.GameInterop.Console;
            console.WriteLine(string.Format("MESSAGE: {0} in Sleep", asleepSpan.ToString("hh':'mm':'ss''")));

            return new ConsciousnessAwake(consciousness);
        }

        #endregion Conversions

        public override IConsciousnessState UpdateState(Consciousness consciousness)
        {
            return this;
        }
    }
}