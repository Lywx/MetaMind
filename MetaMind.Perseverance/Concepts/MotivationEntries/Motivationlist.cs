using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MetaMind.Perseverance.Concepts.MotivationEntries
{
    public enum MotivationSpace
    {
        Past, Now, Future,
    }

    [DataContract]
    public class Motivationlist
    {
        public Motivationlist()
        {
            this.PastMotivations   = new List<MotivationEntry>();
            this.NowMotivations    = new List<MotivationEntry>();
            this.FutureMotivations = new List<MotivationEntry>();
        }

        [DataMember]
        public List<MotivationEntry> FutureMotivations { get; private set; }

        [DataMember]
        public List<MotivationEntry> NowMotivations { get; private set; }

        [DataMember]
        public List<MotivationEntry> PastMotivations { get; private set; }

        public MotivationEntry Create(MotivationSpace space)
        {
            var entry = new MotivationEntry();
            switch (space)
            {
                case MotivationSpace.Past:
                    {
                        this.PastMotivations.Add(entry);
                        break;
                    }

                case MotivationSpace.Now:
                    {
                        this.NowMotivations.Add(entry);
                        break;
                    }

                case MotivationSpace.Future:
                    {
                        this.FutureMotivations.Add(entry);
                        break;
                    }
            }

            return entry;
        }

        public void Remove(MotivationEntry entry, MotivationSpace space)
        {
            switch (space)
            {
                case MotivationSpace.Past:
                    {
                        this.PastMotivations.Remove(entry);
                        break;
                    }

                case MotivationSpace.Now:
                    {
                        this.NowMotivations.Remove(entry);
                        break;
                    }

                case MotivationSpace.Future:
                    {
                        this.FutureMotivations.Remove(entry);
                        break;
                    }
            }
        }
    }
}