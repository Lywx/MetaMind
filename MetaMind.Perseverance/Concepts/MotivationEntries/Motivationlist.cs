// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Motivationlist.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance.Concepts.MotivationEntries
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.Serialization;

    public enum MotivationSpace
    {
        Past, 
        Now, 
        Future, 
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
                        this.RemoveInMotivations(this.PastMotivations, entry);
                        break;
                    }

                case MotivationSpace.Now:
                    {
                        this.RemoveInMotivations(this.NowMotivations, entry);
                        break;
                    }

                case MotivationSpace.Future:
                    {
                        this.RemoveInMotivations(this.FutureMotivations, entry);
                        break;
                    }
            }
        }

        private void RemoveInMotivations(List<MotivationEntry> motivations, MotivationEntry entry)
        {
            if (motivations.Contains(entry))
            {
                motivations.Remove(entry);
            }
            else
            {
                Debug.WriteLine("Warning: Unnecessary removal of entry {0} in motivation {1}", motivations, entry.Name);
            }
        }
    }
}