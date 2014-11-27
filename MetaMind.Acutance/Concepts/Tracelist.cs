namespace MetaMind.Acutance.Concepts
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class Tracelist
    {
        public Tracelist()
        {
            this.Traces = new List<TraceEntry>();
        }

        [DataMember]
        public List<TraceEntry> Traces { get; private set; }

        public TraceEntry Create()
        {
            var entry = new TraceEntry();
            this.Traces.Add(entry);
            return entry;
        }

        public void Remove(TraceEntry entry)
        {
            if (this.Traces.Contains(entry))
            {
                this.Traces.Remove(entry);
            }
        }

        public void Update()
        {
            foreach (var trace in this.Traces.ToArray())
            {
                trace.Update();
            }
        }
    }
}