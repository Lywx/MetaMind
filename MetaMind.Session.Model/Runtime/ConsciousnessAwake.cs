namespace MetaMind.Session.Model.Runtime
{
    using System;

    [DataContract]
    internal class ConsciousnessAwake : ConsciousnessState, IConsciousnessAwake
    {
        public ConsciousnessAwake(Consciousness consciousness)
            : base(consciousness) {}

        public TimeSpan AwakeSpan
        {
            get
            {
                var result = this.Consciousness.HasSlept
                            ? DateTime.Now - this.Consciousness.LastSleepEndTime
                            : DateTime.Now - this.Consciousness.KnownOriginTime;

                return result;
            }
        }

        public override IConsciousnessState UpdateState(
            Consciousness consciousness)
        {
            return this;
        }

        public ConsciousnessAsleep Sleep(Consciousness consciousness)
        {
            consciousness.LastSleepBeginTime = DateTime.Now;

            consciousness.KnownAwakeDuration += this.AwakeSpan;

            var console = this.Interop.Console;
            console.WriteLine(
                $"MESSAGE: Awake {this.AwakeSpan.ToString("hh':'mm':'ss''")}");

            var @event = this.Interop.Event;
            @event.TriggerEvent(
                new MMEvent(
                    (int)SessionEvent.SleepStarted,
                    new ConsciousnessSleepStartedEventArgs(consciousness)));

            return new ConsciousnessAsleep(consciousness);
        }
    }
}
