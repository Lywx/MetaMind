namespace MetaMind.Acutance.Concepts
{
    using System;
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
        public bool Positive = true;

        [DataMember]
        public string Name = string.Empty;

        [DataMember]
        public bool Stopped;

        [DataMember(IsRequired = true)]
        public Experience TransExperience;

        private Stopwatch timer;

        public TraceEntry()
        {
            this.Reset();
            this.Start();

            this.timer = new Stopwatch();
            this.timer.Start();
        }

        [OnDeserialized]
        public void OnDeserialized(StreamingContext context)
        {
            this.timer = new Stopwatch();
            this.timer.Start();
        }

        public void Reset()
        {
            this.Experience = Experience.Zero;
        }

        public void Start()
        {
            this.TransExperience = new Experience();
        }

        public void Update()
        {
            this.Experience      += this.timer.Elapsed;
            this.TransExperience += this.timer.Elapsed;

            this.timer.Restart();

            if (this.TransExperience.CertainDuration >= TimeSpan.FromSeconds(20))
            {
                Stopped = true;
            }
        }
    }
}