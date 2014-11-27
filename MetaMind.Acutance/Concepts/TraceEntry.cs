namespace MetaMind.Acutance.Concepts
{
    using System.Diagnostics;
    using System.Runtime.Serialization;

    using MetaMind.Engine;
    using MetaMind.Engine.Concepts;

    [DataContract]
    public class TraceEntry : EngineObject
    {
        [DataMember]
        public Experience Experience;

        [DataMember]
        public string Name = string.Empty;

        private Stopwatch timer;

        [OnDeserialized]
        public void OnDeserialized(StreamingContext context)
        {
            this.timer = new Stopwatch();
            this.timer.Start();
        }

        public TraceEntry()
        {
            this.Experience = Experience.Zero;

            this.timer = new Stopwatch();
            this.timer.Start();
        }

        public void Update()
        {
            this.Experience += this.timer.Elapsed;
            
            this.timer.Restart();
        }
    }
}