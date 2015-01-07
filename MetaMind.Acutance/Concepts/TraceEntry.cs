namespace MetaMind.Acutance.Concepts
{
    using System.Diagnostics;
    using System.Runtime.Serialization;

    using MetaMind.Engine;
    using MetaMind.Engine.Concepts;

    [DataContract]
    public class TraceEntry : EngineObject
    {
        [DataMember(IsRequired = true)]
        public Experience Experience;

        [DataMember]
        public string Name = string.Empty;

        private Stopwatch timer;

        public TraceEntry()
        {
            this.Experience = Experience.Zero;

            this.timer = new Stopwatch();
            this.timer.Start();
        }

        ~TraceEntry()
        {
            
        }

        public override void Dispose()
        {
            if (this.timer != null)
            {
                this.timer.Stop();
            }

            this.timer = null;

            this.Name       = null;
            this.Experience = null;
        }

        [OnDeserialized]
        public void OnDeserialized(StreamingContext context)
        {
            this.timer = new Stopwatch();
            this.timer.Start();
        }

        public virtual void Reset()
        {
            this.Experience = Experience.Zero;
        }

        public void Update()
        {
            this.Experience += this.timer.Elapsed;

            this.timer.Restart();
        }
    }
}