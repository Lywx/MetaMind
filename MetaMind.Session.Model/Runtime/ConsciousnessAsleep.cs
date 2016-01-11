namespace MetaMind.Session.Model.Runtime
{
    using System;

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

            var asleepDuration = consciousness.LastSleepEndTime - consciousness.LastSleepBeginTime;
            consciousness.KnownAsleepDuration += asleepDuration;

            this.AwakeInterop(consciousness, asleepDuration);

            return new ConsciousnessAwake(consciousness);
        }

        private void AwakeInterop(Consciousness consciousness, TimeSpan asleepDuration)
        {
            this.Interop.Event.TriggerEvent(
                new MMEvent(
                    (int)SessionEvent.SleepStopped,
                    new ConsciousnessSleepStoppedEventArgs(consciousness)));

            this.Interop.Console.WriteLine(
                $"MESSAGE: Asleep {asleepDuration.ToString("hh':'mm':'ss''")}");
        }

        #endregion Conversions

        public override IConsciousnessState UpdateState(Consciousness consciousness)
        {
            return this;
        }
    }
}