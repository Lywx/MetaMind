namespace MetaMind.Engine.Node.Actions.Intervals
{
    public class MMRotateByState : MMFiniteTimeActionState
    {
        #region Constructors

        public MMRotateByState (MMRotateBy action, MMNode target)
            : base (action, target)
        { 
            this.AngleX = action.AngleX;
            this.AngleY = action.AngleY;

            this.StartAngleX = target.RotationX;
            this.StartAngleY = target.RotationY;
        }

        #endregion

        #region

        protected float AngleX { get; set; }

        protected float AngleY { get; set; }

        protected float StartAngleX { get; set; }

        protected float StartAngleY { get; set; }

        #endregion

        public override void Update(float time)
        {
            // XXX: shall I add % 360
            if (this.Target != null)
            {
                this.Target.RotationX = this.StartAngleX + this.AngleX * time;
                this.Target.RotationY = this.StartAngleY + this.AngleY * time;
            }
        }
    }
}