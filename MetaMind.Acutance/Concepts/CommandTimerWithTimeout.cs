namespace MetaMind.Acutance.Concepts
{
    using System;
    using System.Diagnostics;
    using System.Runtime.Serialization;

    using MetaMind.Engine.Concepts;

    [DataContract]
    public class CommandTimerWithTimeout : CommandTimer
    {
        private Stopwatch timer;

        public CommandTimerWithTimeout(TimeSpan timeout)
        {
            this.Timeout = timeout;
        }

        public override bool Transiting
        {
            get { return this.Experience.CertainDuration >= this.Timeout; }
        }

        [DataMember]
        private TimeSpan Timeout { get; set; }

        public override void Reset()
        {
            this.timer = new Stopwatch();
            this.timer.Start();

            this.Experience = Experience.Zero;
        }

        public override void Update()
        {
            this.Experience += this.timer.Elapsed;

            this.timer.Restart();
        }
    }
}