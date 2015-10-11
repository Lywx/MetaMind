namespace MetaMind.Engine.Entities.Nodes.Actions.Intervals
{
    public class MMScaleToState : MMFiniteTimeActionState
    {
        protected float DeltaX;
        protected float DeltaY;
        protected float EndScaleX;
        protected float EndScaleY;
        protected float StartScaleX;
        protected float StartScaleY;

        public MMScaleToState (MMScaleTo action, MMNode target)
            : base (action, target)
        { 
            this.StartScaleX = target.ScaleX;
            this.StartScaleY = target.ScaleY;
            this.EndScaleX = action.EndScaleX;
            this.EndScaleY = action.EndScaleY;
            this.DeltaX = this.EndScaleX - this.StartScaleX;
            this.DeltaY = this.EndScaleY - this.StartScaleY;
        }

        public override void Update (float time)
        {
            if (this.Target != null)
            {
                this.Target.ScaleX = this.StartScaleX + this.DeltaX * time;
                this.Target.ScaleY = this.StartScaleY + this.DeltaY * time;
            }
        }
    }

    public class MMScaleTo : MMFiniteTimeAction
    {
        #region Constructors

        public MMScaleTo (float duration, float scale) : this (duration, scale, scale)
        {
        }

        public MMScaleTo (float duration, float scaleX, float scaleY) : base (duration)
        {
            this.EndScaleX = scaleX;
            this.EndScaleY = scaleY;
        }

        #endregion Constructors

        public float EndScaleX { get; private set; }

        public float EndScaleY { get; private set; }

        public override MMFiniteTimeAction Reverse()
        {
            throw new System.NotImplementedException ();
        }

        protected internal override MMActionState StartAction(IMMNode target)
        {
            return new MMScaleToState (this, target);
        }
    }
}