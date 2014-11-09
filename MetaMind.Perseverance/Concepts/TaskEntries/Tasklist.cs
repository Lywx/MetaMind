using System;
using System.Collections.Generic;

namespace MetaMind.Perseverance.Concepts.TaskEntries
{
    [Serializable]
    public class Tasklist
    {
        public List<TaskEntry> Tasks { get; private set; }

        #region Constructors

        public Tasklist()
        {
            Tasks = new List<TaskEntry>();
        }

        #endregion Constructors

        #region Operations

        public TaskEntry Create()
        {
            var entry = new TaskEntry();
            Tasks.Add( entry );
            return entry;
        }

        public void Remove( TaskEntry entry )
        {
            Tasks.Remove( entry );
        }

        #endregion Operations
    }
}