using System;
using MetaMind.Perseverance.Concepts.TaskEntries;

namespace MetaMind.Perseverance.Concepts.Cognitions
{
    public class SynchronizationStartedEventArgs : EventArgs
    {
        public readonly TaskEntry TaskEntry;

        public SynchronizationStartedEventArgs( TaskEntry data )
        {
            TaskEntry = data;
        }
    }
}