using System.Runtime.Serialization;

namespace MetaMind.Perseverance.Concepts.MotivationEntries
{
    [DataContract]
    public class MotivationEntry
    {
        [DataMember]
        public string Name;

        [DataMember]
        public string Property;

        public MotivationEntry()
        {
            Name     = "";
            Property = "Neutral";
        }
    }
}