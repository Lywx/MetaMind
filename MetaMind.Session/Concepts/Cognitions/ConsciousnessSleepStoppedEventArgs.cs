namespace MetaMind.Session.Concepts.Cognitions
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
