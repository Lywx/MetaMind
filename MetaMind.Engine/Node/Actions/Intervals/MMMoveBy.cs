﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>

namespace MetaMind.Engine.Node.Actions.Intervals
{
    using Geometry;
    using Microsoft.Xna.Framework;

    public class MMMoveBy : MMFiniteTimeAction
    {
        #region Constructors

        public MMMoveBy(float duration, Point deltaLocation) : base(duration)
        {
            this.DeltaLocation = deltaLocation;
        }

        #endregion Constructors

        public Point DeltaLocation { get; private set; }

        protected internal override MMActionState StartAction(MMNode target)
        {
            return new MMMoveByState(this, target);
        }

        public override MMFiniteTimeAction Reverse()
        {
            return new MMMoveBy(this.Duration, -this.DeltaLocation);
        }
    }
}