namespace MetaMind.Session.Concepts.Tests
{
    using System;

    public class TestTimer : IDisposable
    {
        private TimeSpan transitionTimeout = TimeSpan.FromSeconds(1.0);

        private DateTime transitionMoment;

        private TimeSpan passedSpan;

        private long remainSeconds;

        public TestTimer(DateTime initial, TimeSpan period)
        {
            this.Initial = initial;
            this.Period  = period;
        }

        private DateTime Initial { get; set; }

        private TimeSpan Period { get; }

        private bool Transitioning
        {
            get
            {
                return DateTime.Now - this.transitionMoment < this.transitionTimeout;
            }
        }

        #region Events

        public event EventHandler Fired = delegate { };

        #endregion

        #region Update

        public void Update()
        {
            this.UpdateClock();
            this.UpdateState();
        }

        private void UpdateState()
        {
            // Stop firing when transitioning because it cause repetitive firing.
            if (!this.Transitioning && this.remainSeconds == 0)
            {
                this.transitionMoment = DateTime.Now;

                this.Fired(this, EventArgs.Empty);
            }
        }

        private void UpdateClock()
        {
            this.passedSpan = DateTime.Now - this.Initial;

            Math.DivRem(
                (long)(this.passedSpan.TotalSeconds),
                (long)(this.Period.TotalSeconds),
                out this.remainSeconds);
        }

        #endregion

        #region Operations

        public void Reset()
        {
            this.passedSpan = TimeSpan.Zero;
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            this.Fired = null;
        }

        #endregion
    }
}