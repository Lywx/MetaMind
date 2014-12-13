namespace MetaMind.Acutance.Concepts
{
    using System;
    using System.Diagnostics;
    using System.Runtime.Serialization;

    using MetaMind.Engine;

    [DataContract]
    public class EventEntry : EngineObject
    {
        [DataMember]
        public string Name = string.Empty;

        [DataMember]
        public TimeSpan Time;

        [DataMember]
        private string path = string.Empty;

        [DataMember]
        private EventState state = EventState.Running;

        [DataMember]
        private TimeSpan timeout;

        private Stopwatch timer;

        [DataMember]
        private TimeSpan transition = TimeSpan.FromSeconds(30);

        public EventEntry(TimeSpan timeout)
        {
            this.timeout = timeout;

            this.Reset();

            this.timer = new Stopwatch();
            this.timer.Start();
        }

        private enum EventState
        {
            Running, Transiting,
        }

        [OnDeserialized]
        public void OnDeserialized(StreamingContext context)
        {
            this.timer = new Stopwatch();
            this.timer.Start();
        }

        public void Update()
        {
            this.UpdateTimer();
            this.UpdateState();
        }

        private void Reset()
        {
            this.Time = TimeSpan.Zero;
        }

        private void UpdateState()
        {
            switch (this.state)
            {
                case EventState.Running:
                    {
                        if (this.Time >= this.timeout)
                        {
                            this.state = EventState.Transiting;
                        }
                    }

                    break;

                case EventState.Transiting:
                    {
                        if (this.Time >= this.timeout + this.transition)
                        {
                            this.state = EventState.Running;

                            this.Reset();
                        }
                    }

                    break;
            }
        }

        private void UpdateTimer()
        {
            this.Time += this.timer.Elapsed;

            this.timer.Restart();
        }
    }
}