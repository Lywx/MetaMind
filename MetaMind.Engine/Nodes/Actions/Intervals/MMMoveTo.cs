namespace MetaMind.Engine.Nodes.Actions.Intervals
{
    using Microsoft.Xna.Framework;

    public class MMMoveToState : MMMoveByState
    {
        public MMMoveToState(MMMoveTo action, IMMNode target)
            : base(action, target)
        {
            this.StartLocation = target.Location;
            this.DeltaLocation = action.EndLocation - target.Location;
        }

        public override void Update(float time)
        {
            if (this.Target != null)
            {
                var newLocation = this.StartLocation + this.DeltaLocation * time;

                this.Target.Location = newLocation;
                this.PreviousLocation = newLocation;
            }
        }
    }

    public class MMMoveTo : MMMoveBy
    {
        #region Constructors

        public MMMoveTo(float duration, Point location)
            : base(duration, location)
        {
            this.EndLocation = location;
        }

        #endregion Constructors

        public Point EndLocation { get; private set; }

        #region Operations

        protected internal override MMActionState StartAction(IMMNode target)
        {
            return new MMMoveToState(this, target);
        }

        #endregion
    }
}
