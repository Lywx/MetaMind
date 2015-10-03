namespace CocosSharp
{
    using MetaMind.Engine.Node;
    using MetaMind.Engine.Node.Actions;

    public class MMBlinkState : MMFiniteTimeActionState
    {
        public MMBlinkState(MMBlink action, MMNode target)
            : base(action, target)
        {
            this.Times         = action.Times;
            this.OriginalState = target.Visible;
        }

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
                this.Target.Visible = m > (slice / 2);
            }
        }

        protected internal override void Stop()
        {
            this.Target.Visible = this.OriginalState;
            base.Stop();
        }
    }
}
