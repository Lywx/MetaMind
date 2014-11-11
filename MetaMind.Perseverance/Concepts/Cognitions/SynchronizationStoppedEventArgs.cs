using MetaMind.Perseverance.Concepts.TaskEntries;
using System;

namespace MetaMind.Perseverance.Concepts.Cognitions
{
    public class SynchronizationStoppedEventArgs : EventArgs
    {
        public readonly TaskEntry TaskEntry;

        public SynchronizationStoppedEventArgs( TaskEntry data )
        {
            TaskEntry = data;
        }
    }
}