namespace MetaMind.Engine.Nodes.Actions.Intervals
{
    using Microsoft.Xna.Framework;

    public class MMMoveByState : MMFiniteTimeActionState
    {
        public MMMoveByState(MMMoveBy action, IMMNode target)
            : base(action, target)
        {
            this.DeltaLocation = action.DeltaLocation;
            this.PreviousLocation = this.StartLocation = target.Location;
        }

        protected Point PreviousLocation { get; set; }

        protected Point DeltaLocation { get; set; }

        protected Point StartLocation { get; set; }

        protected Point EndLocation { get; set; }

        public override void Update(float time)
        {
            if (this.Target == null)
            {
                return;
            }

            var deltaLocation = this.Target.Location - this.PreviousLocation;

            // Update start location per update
            this.StartLocation = this.StartLocation + deltaLocation;

            var newLocation = this.StartLocation + this.DeltaLocation * time;
            this.Target.Location = newLocation;
            this.PreviousLocation = newLocation;
        }
    }

    public class MMMoveBy : MMFiniteTimeAction
    {
        #region Constructors

        public MMMoveBy(float duration, Point deltaLocation) : base(duration)
        {
            this.DeltaLocation = deltaLocation;
        }

        #endregion Constructors

        public Point DeltaLocation { get; }

        protected internal override MMActionState StartAction(IMMNode target)
        {
            return new MMMoveByState(this, target);
        }

        public override MMFiniteTimeAction Reverse()
        {
            return new MMMoveBy(this.Duration, -this.DeltaLocation);
        }
    }
}
