namespace MetaMind.Acutance.Concepts
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class Calllist
    {
        public Calllist()
        {
            this.Calls = new List<CallEntry>();
        }

        [DataMember]
        public List<CallEntry> Calls { get; private set; }

        public CallEntry Create(string name, string path, int minutes)
        {
            var entry = new CallEntry(name, path, TimeSpan.FromMinutes(minutes));
            this.Calls.Add(entry);
            return entry;
        }

        public void Remove(CallEntry entry)
        {
            if (this.Calls.Contains(entry))
            {
                this.Calls.Remove(entry);
            }
        }

        public void Update()
        {
            foreach (var @event in this.Calls.ToArray())
            {
                @event.Update();
            }
        }
    }
}