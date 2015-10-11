namespace MetaMind.Engine.Entities.Nodes.Actions.Intervals
{
    public class MMScaleByState : MMScaleToState
    {

        public MMScaleByState (MMScaleTo action, MMNode target)
            : base (action, target)
        { 
            this.DeltaX = this.StartScaleX * this.EndScaleX - this.StartScaleX;
            this.DeltaY = this.StartScaleY * this.EndScaleY - this.StartScaleY;
        }

    }

    public class MMScaleBy : MMScaleTo
    {
        #region Constructors

        public MMScaleBy(float duration, float scale) : base(duration, scale) {}

        public MMScaleBy(float duration, float scaleX, float scaleY)
            : base(duration, scaleX, scaleY) {}

        #endregion Constructors

        #region Operations

        protected internal override MMActionState StartAction(IMMNode target)
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
