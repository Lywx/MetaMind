using System;

namespace MetaMind.Perseverance.Concepts.Cognitions
{
    public class ConsciousnessSleepStartedEventArgs : EventArgs
    {
        public readonly ConsciousnessAwake State;

        public ConsciousnessSleepStartedEventArgs( ConsciousnessAwake state )
        {
            State = state;
        }
    }
}