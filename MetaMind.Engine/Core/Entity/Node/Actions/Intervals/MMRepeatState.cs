namespace MetaMind.Engine.Core.Entity.Node.Actions.Intervals
{
    using Entity.Node.Model;

    public class MMRepeatState : MMFiniteTimeActionState
    {
        #region Constructors

        public MMRepeatState(MMRepeat action, IMMNode target)
            : base(action, target)
        {
            this.InnerAction = action.InnerAction;
            this.InnerActionState = (MMFiniteTimeActionState)this.InnerAction.StartAction(target);

            this.Times = action.Times;
            this.Count = action.Count;

            this.NextDt = this.InnerAction.Duration / this.Duration;
        }

        #endregion

        public bool IsInstant => (this.InnerAction is MMRepeat) && ((MMRepeat)this.InnerAction).IsInstant;

        public override bool IsDone => this.Count == this.Times;

        protected float NextDt { get; set; }

        protected MMFiniteTimeAction InnerAction { get; set; }

        protected MMFiniteTimeActionState InnerActionState { get; set; }

        protected uint Times { get; set; }

        protected uint Count { get; set; }

        protected internal override void Stop()
        {
            this.InnerActionState.Stop();
            base                 .Stop();
        }

        public override void Update(float time)
        {
            // When the repeat should be finished
            if (time >= this.NextDt)
            {
                // When the repeat duration is reached but the times remain incomplete 
                while (time > this.NextDt
                       && this.Count < this.Times)
                {
                    // Do extra work to complete 1 time in this frame
                    this.InnerActionState.Update(1.0f);

                    ++this.Count;

                    this.InnerActionState.Stop();
                    this.InnerActionState = (MMFiniteTimeActionState)this.InnerAction.StartAction(this.Target);

                    // TODO(Unknown):
                    this.NextDt = this.InnerAction.Duration / this.Duration * (this.Count + 1f);
                }

                // fix for issue #1288, incorrect end value of repeat
                if (time >= 1.0f
                    && this.Count < this.Times)
                {
                    ++this.Count;
                }

                // don't set an instant action back or update it, it has no use because it has no duration
                if (!this.IsInstant)
                {
                    if (this.Count == this.Times)
                    {
                        this.InnerActionState.Update(1f);
                        this.InnerActionState.Stop();
                    }
                    else
                    {
                        // issue #390 prevent jerk, use right update
                        this.InnerActionState.Update(time - (this.NextDt - this.InnerAction.Duration / this.Duration));
                    }
                }
            }
            else
            {
                this.InnerActionState.Update((time * this.Times) % 1.0f);
            }
        }
    }
}