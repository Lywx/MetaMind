namespace MetaMind.Engine.Core.Entity.Node.Actions.Intervals
{
    using Entity.Node.Model;

    public class MMBlinkState : MMFiniteTimeActionState
    {
        #region Constructors

        public MMBlinkState(MMBlink action, IMMNode target)
            : base(action, target)
        {
            this.Times         = action.Times;
            this.OriginalState = target.EntityVisible;
        }

        #endregion

        protected uint Times { get; set; }

        protected bool OriginalState { get; set; }

        public override void Update(float time)
        {
            if (this.Target != null
                && !this.IsDone)
            {
                var slice = 1.0f / this.Times;

                // float m = fmodf(time, slice);
                var m = time % slice;
                this.Target.EntityVisible = m > (slice / 2);
            }
        }

        protected internal override void Stop()
        {
            this.Target.EntityVisible = this.OriginalState;
            base.Stop();
        }
    }

    public class MMBlink : MMFiniteTimeAction
    {
        #region Constructors

        public MMBlink(float duration, uint timeOfBlinks) : base(duration)
        {
            this.Times = timeOfBlinks;
        }

        #endregion Constructors

        public uint Times { get; }

        protected internal override MMActionState StartAction(IMMNode target)
        {
            return new MMBlinkState(this, target);
        }

        public override MMFiniteTimeAction Reverse()
        {
            return new MMBlink(this.Duration, this.Times);
        }
    }
}
