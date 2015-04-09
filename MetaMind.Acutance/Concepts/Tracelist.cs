namespace MetaMind.Acutance.Concepts
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    public interface ITracelist
    {
        [DataMember]
        List<Trace> Traces { get; }

        Trace Create();

        void Remove(Trace entry);

        void Update();
    }

    [DataContract]
    public class Tracelist : ITracelist
    {
        public Tracelist()
        {
            this.Traces = new List<Trace>();
        }

        [DataMember]
        public List<Trace> Traces { get; private set; }

        public Trace Create()
        {
            var entry = new Trace();
            this.Traces.Add(entry);
            return entry;
        }

        public void Remove(Trace entry)
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