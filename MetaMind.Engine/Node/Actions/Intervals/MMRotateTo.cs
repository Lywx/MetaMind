// --------------------------------------------------------------------------------------------------------------------
// <copyright file="">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>

namespace MetaMind.Engine.Node.Actions.Intervals
{
    using System;

    public class MMRotateTo : MMFiniteTimeAction
    {
        public float DistanceAngleX { get; private set; }
        public float DistanceAngleY { get; private set; }


        #region Constructors

        public MMRotateTo (float duration, float deltaAngleX, float deltaAngleY) : base (duration)
        {
            this.DistanceAngleX = deltaAngleX;
            this.DistanceAngleY = deltaAngleY;
        }

        public MMRotateTo (float duration, float deltaAngle) : this (duration, deltaAngle, deltaAngle)
        {
        }

        #endregion Constructors

        protected internal override MMActionState StartAction(MMNode target)
        {
            return new MMRotateToState (this, target);
        }

        public override MMFiniteTimeAction Reverse()
        {
            throw new NotImplementedException();
        }
    }
}