// --------------------------------------------------------------------------------------------------------------------
// <copyright file="">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
namespace MetaMind.Engine.Nodes.Actions.Intervals
{
    public class MMRotateByState : MMFiniteTimeActionState
    {
        #region Constructors

        public MMRotateByState (MMRotateBy action, IMMNode target)
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

    public class MMRotateBy : MMFiniteTimeAction
    {
        public float AngleX { get; private set; }

        public float AngleY { get; private set; }

        #region Constructors

        public MMRotateBy (float duration, float deltaAngleX, float deltaAngleY) : base (duration)
        {
            this.AngleX = deltaAngleX;
            this.AngleY = deltaAngleY;
        }

        public MMRotateBy (float duration, float deltaAngle) : this (duration, deltaAngle, deltaAngle)
        {
        }

        #endregion Constructors

        protected internal override MMActionState StartAction(IMMNode target)
        {
            return new MMRotateByState(this, target);
        }

        public override MMFiniteTimeAction Reverse()
        {
            return new MMRotateBy(this.Duration, -this.AngleX, -this.AngleY);
        }
    }
}