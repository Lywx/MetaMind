namespace MetaMind.Engine.Entities.Nodes
{
    using System;

    // TODO:
    internal class CCTimer : ICCUpdatable
    {
        #region Constructors

        public CCTimer(
            CCScheduler scheduler,
            ICCUpdatable target,
            Action<float> selector)
            : this(scheduler, target, selector, 0, 0, 0) {}

        public CCTimer(
            CCScheduler scheduler,
            ICCUpdatable target,
            Action<float> selector,
            float seconds)
            : this(scheduler, target, selector, seconds, 0, 0) {}

        public CCTimer(
            CCScheduler scheduler,
            ICCUpdatable target,
            Action<float> selector,
            float seconds,
            uint repeat,
            float delay)
        {
            this.Scheduler = scheduler;
            this.Target    = target;
            this.Selector  = selector;

            this.OriginalInterval = seconds;
            this.Interval         = seconds;

            this.Delay    = delay;
            this.UseDelay = delay > 0f;

            // Default value for not updated  
            this.elapsed = -1;

            this.Repeat     = repeat;
            this.RunForever = repeat == uint.MaxValue;
        }

        #endregion Constructors

        #region Interval Data

        public float OriginalInterval { get; internal set; }

        public float Interval { get; set; }

        #endregion

        #region Repetition Data

        private uint timesExecuted;

        /// <remarks>
        /// 0 = once, 1 is twice time executed, 2 is third time, etc.
        /// </remarks>
        public uint Repeat { get; }

        #endregion

        #region Timer Data

        private float elapsed;

        public float Delay { get; }

        public bool RunForever { get; }

        public bool UseDelay { get; private set; }

        #endregion

        #region

        public Action<float> Selector { get; set; }

        private CCScheduler Scheduler { get; }

        public ICCUpdatable Target { get; }

        #endregion

        #region Update

        public void Update(float dt)
        {
            // When update for the first time
            if (this.elapsed == -1)
            {
                this.elapsed = 0;
                this.timesExecuted = 0;
            }
            else
            {
                // Standard timer usage: when run forever and not use delay
                if (this.RunForever && 
                   !this.UseDelay)
                {
                    this.elapsed += dt;

                    if (this.elapsed >= this.Interval)
                    {
                        this.Selector?.Invoke(this.elapsed);

                        this.elapsed = 0;
                    }
                }

                // Advanced time usage: when use delay
                else
                {
                    this.elapsed += dt;

                    if (this.UseDelay)
                    {
                        // Detect when delay is reached
                        if (this.elapsed >= this.Delay)
                        {
                            this.Selector?.Invoke(this.elapsed);

                            this.elapsed = this.elapsed - this.Delay;

                            this.timesExecuted += 1;
                            this.UseDelay = false;
                        }
                    }
                    else
                    {
                        // Detect when the interval is reached
                        if (this.elapsed >= this.Interval)
                        {
                            this.Selector?.Invoke(this.elapsed);

                            // this.Interval = this.OriginalInterval - (this.elapsed - this.Interval);

                            // Reset elapse data
                            this.elapsed = 0;
                            this.timesExecuted += 1;
                        }
                    }

                    if (this.timesExecuted > this.Repeat && 
                       !this.RunForever)
                    {
                        this.Scheduler.Unschedule(this.Selector, this.Target);
                    }
                }
            }
        }

        #endregion
    }
}
