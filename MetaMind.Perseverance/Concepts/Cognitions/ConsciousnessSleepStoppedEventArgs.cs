namespace MetaMind.Perseverance.Concepts.Cognitions
{
    public class ConsciousnessSleepStoppedEventArgs
    {
        public readonly ConsciousnessSleepy State;

        public ConsciousnessSleepStoppedEventArgs( ConsciousnessSleepy state )
        {
            State = state;
        }
    }
}