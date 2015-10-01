// --------------------------------------------------------------------------------------------------------------------
// <copyright file="">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
namespace MetaMind.Engine.Node.Actions.Intervals
{
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

        protected internal override MMActionState StartAction(MMNode target)
        {
            return new MMRotateByState(this, target);
        }

        public override MMFiniteTimeAction Reverse()
        {
            return new MMRotateBy(this.Duration, -this.AngleX, -this.AngleY);
        }
    }
}