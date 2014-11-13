using MetaMind.Perseverance.Concepts.TaskEntries;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MetaMind.Perseverance.Concepts.MotivationEntries
{
    using MetaMind.Perseverance.Guis.Modules;

    [DataContract]
    public class MotivationEntry
    {
        [DataMember]
        public string Name = string.Empty;

        [DataMember]
        public string Property = "Neutral";

        [DataMember]
        public List<TaskEntry> Tasks = new List<TaskEntry>();

        public MotivationEntry()
        {
        }

        public void CopyTo(MotivationSpace space, int position)
        {
            switch (space)
            {
                case MotivationSpace.Past:
                    {
                        MotivationExchangeSettings.GetPastMotivations().Insert(position, this);
                        break;
                    }
                case MotivationSpace.Now:
                    {
                        MotivationExchangeSettings.GetNowMotivations().Insert(position, this);
                        break;
                    }
                case MotivationSpace.Future:
                    {
                        MotivationExchangeSettings.GetFutureMotivations().Insert(position, this);
                        break;
                    }
            }
        }
    }
}