namespace MetaMind.Session.Model.Runtime
{
    public class ConsciousnessSleepStoppedEventArgs
    {
        public ConsciousnessSleepStoppedEventArgs(Consciousness consciousness)
        {
            this.Consciousness = consciousness;
        }

        public Consciousness Consciousness { get; }
    }
}
