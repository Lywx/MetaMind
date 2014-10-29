using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MetaMind.Perseverance.Concepts.MotivationEntries
{
    [DataContract]
    public class MotivationEntry
    {
        [DataMember]
        public string Name { get; set; }

        
        public MotivationEntry()
        {
            
        }
    }
}
