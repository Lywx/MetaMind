using MetaMind.Perseverance.Concepts.TaskEntries;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MetaMind.Perseverance.Concepts.MotivationEntries
{
    [DataContract]
    public class MotivationEntry
    {
        [DataMember] public string          Name = "";
        [DataMember] public string          Property = "Neutral";

        [DataMember] public List<TaskEntry> Tasks = new List<TaskEntry>();

        public MotivationEntry()
        {
        }
    }
}