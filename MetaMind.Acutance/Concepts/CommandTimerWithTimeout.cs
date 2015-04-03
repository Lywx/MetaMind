namespace MetaMind.Acutance.Concepts
{
    using System;
    using System.Diagnostics;
    using System.Runtime.Serialization;

    using MetaMind.Engine.Concepts;

    [DataContract]
    public class CommandTimerWithTimeout : CommandTimer
    {
        [DataMember]
        private bool isAutoReseting;

        private Stopwatch timer;

        public CommandTimerWithTimeout(TimeSpan timeout, CommandRepeativity repeativity)
            : this(timeout)
        {
            this.isAutoReseting = repeativity == CommandRepeativity.EveryMoment;
        }

        public CommandTimerWithTimeout(TimeSpan timeout)
        {
            this.Timeout = timeout;
        }

        ~CommandTimerWithTimeout()
        {
            this.Dispose();
        }

        public override void Dispose()
        {
            if (this.timer != null)
            {
                this.timer.Stop();
            }

            this.timer = null;

            base.Dispose();
        }

        public override bool IsTransiting
        {
            get { return this.Experience.CertainDuration >= this.Timeout; }
        }

        public override bool IsAutoReseting
        {
            get { return this.isAutoReseting; }
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
            // disposal guard
            if (this.timer != null)
            {
                this.Experience += this.timer.Elapsed;

                this.timer.Restart();
            }
        }
    }
}