namespace MetaMind.Perseverance.Concepts.MotivationEntries
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    using MetaMind.Perseverance.Concepts.TaskEntries;
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

        public void CopyToSpace(MotivationSpace space, int position)
        {
            var source = MotivationExchangeSettings.GetMotivationSource(space);
            if (source != null)
            {
                source.Insert(position, this);
            }
        }

        public void SwapWithInSpace(MotivationSpace space, MotivationEntry target)
        {
            var source = MotivationExchangeSettings.GetMotivationSource(space);
            if (source != null && 
                source.Contains(this) && 
                source.Contains(target))
            {
                var thisIndex   = source.IndexOf(this);
                var targetIndex = source.IndexOf(target);

                source[thisIndex]   = target;
                source[targetIndex] = this;
            }
        }

    }
}