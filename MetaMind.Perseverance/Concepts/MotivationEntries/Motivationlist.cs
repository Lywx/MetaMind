using System.Collections.Generic;
using System.Runtime.Serialization;
using MetaMind.Perseverance.Concepts.TaskEntries;

namespace MetaMind.Perseverance.Concepts.MotivationEntries
{
    [DataContract]
    public class Motivationlist
    {
        [DataMember]
        public List<MotivationEntry> Motivations { get; private set; }

        public Motivationlist()
        {
            Motivations = new List<MotivationEntry>();
        }


        public MotivationEntry Create()
        {
            var entry = new MotivationEntry();
            Motivations.Add( entry );
            return entry;
        }

        public void Remove( MotivationEntry entry )
        {
            Motivations.Remove( entry );
        }
    }
}