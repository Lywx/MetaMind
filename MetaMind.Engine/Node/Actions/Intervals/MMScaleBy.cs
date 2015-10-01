namespace MetaMind.Engine.Node.Actions.Intervals
{
    public class MMScaleBy : MMScaleTo
    {
        #region Constructors

        public MMScaleBy(float duration, float scale) : base(duration, scale) {}

        public MMScaleBy(float duration, float scaleX, float scaleY)
            : base(duration, scaleX, scaleY) {}

        #endregion Constructors

        #region Operations

        protected internal override MMActionState StartAction(MMNode target)
        {
            return new MMScaleByState(this, target);
        }

        public override MMFiniteTimeAction Reverse()
        {
            return new MMScaleBy(
                this.Duration,
                1 / this.EndScaleX,
                1 / this.EndScaleY);
        }

        #endregion
    }
}
