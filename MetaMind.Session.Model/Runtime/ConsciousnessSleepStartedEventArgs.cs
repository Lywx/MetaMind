namespace MetaMind.Session.Model.Runtime
{
    using System;

    public class ConsciousnessSleepStartedEventArgs : EventArgs
    {
        public ConsciousnessSleepStartedEventArgs(Consciousness consciousness)
        {
            this.Consciousness = consciousness;
        }

        public Consciousness Consciousness { get; }
    }
}
