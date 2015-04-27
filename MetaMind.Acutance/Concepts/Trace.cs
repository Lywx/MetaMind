namespace MetaMind.Acutance.Concepts
{
    using System;
    using System.Diagnostics;
    using System.Runtime.Serialization;

    [DataContract]
    public class Trace : IDisposable
    {
        [DataMember(IsRequired = true)]
        public SynchronizationSpan SynchronizationSpan;

        [DataMember]
        public string Name = string.Empty;

        private Stopwatch timer;

        public Trace()
        {
        // FIXME: Won't suit
            this.SynchronizationSpan = SynchronizationSpan.Zero;

            this.timer = new Stopwatch();
            this.timer.Start();
        }

        ~Trace()
        {
            this.Dispose();
        }

        public virtual void Dispose()
        {
            if (this.timer != null)
            {
                this.timer.Stop();
            }
        }

        [OnDeserialized]
        public void OnDeserialized(StreamingContext context)
        {
            this.timer = new Stopwatch();
            this.timer.Start();
        }

        public virtual void Reset()
        {
            this.SynchronizationSpan = SynchronizationSpan.Zero;
        }

        public void Update()
        {
            this.SynchronizationSpan += this.timer.Elapsed;

            this.timer.Restart();
        }
    }
}