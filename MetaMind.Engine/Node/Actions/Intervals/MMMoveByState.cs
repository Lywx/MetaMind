// --------------------------------------------------------------------------------------------------------------------
// <copyright file="">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
namespace MetaMind.Engine.Node.Actions.Intervals
{
    using Geometry;
    using Microsoft.Xna.Framework;

    public class MMMoveByState : MMFiniteTimeActionState
    {
        public MMMoveByState(MMMoveBy action, MMNode target)
            : base(action, target)
        {
            this.DeltaLocation = action.DeltaLocation;
            this.PreviousLocation = this.StartLocation = target.Location;
        }

        protected Point PreviousLocation { get; set; }

        protected Point DeltaLocation { get; set; }

        protected Point StartLocation { get; set; }

        protected Point EndLocation{ get; set; }

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
}