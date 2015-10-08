namespace MetaMind.Engine.Nodes.Actions.Intervals
{
    public class MMTintBy : MMFiniteTimeAction
    {
        #region Constructors

        public MMTintBy(
            float duration,
            short deltaRed,
            short deltaGreen,
            short deltaBlue) : base(duration)
        {
            this.DeltaR = deltaRed;
            this.DeltaG = deltaGreen;
            this.DeltaB = deltaBlue;
        }

        #endregion Constructors

        public short DeltaB { get; }

        public short DeltaG { get; }

        public short DeltaR { get; }

        protected internal override MMActionState StartAction(IMMNode target)
        {
            return new MMTintByState(this, target);
        }

        public override MMFiniteTimeAction Reverse()
        {
            return new MMTintBy(
                this.Duration,
                (short)-this.DeltaR,
                (short)-this.DeltaG,
                (short)-this.DeltaB);
        }
    }
}