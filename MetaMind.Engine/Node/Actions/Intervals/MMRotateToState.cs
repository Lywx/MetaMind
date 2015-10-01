// --------------------------------------------------------------------------------------------------------------------
// <copyright file="">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
namespace MetaMind.Engine.Node.Actions.Intervals
{
    public class MMRotateToState : MMFiniteTimeActionState
    {
        protected float DiffAngleY;
        protected float DiffAngleX;

        protected float DistanceAngleX { get; set; }

        protected float DistanceAngleY { get; set; }

        protected float StartAngleX;
        protected float StartAngleY;

        public MMRotateToState (MMRotateTo action, MMNode target)
            : base (action, target)
        { 
            this.DistanceAngleX = action.DistanceAngleX;
            this.DistanceAngleY = action.DistanceAngleY;

            // Calculate X
            this.StartAngleX = this.Target.RotationX;
            if (this.StartAngleX > 0)
            {
                this.StartAngleX = this.StartAngleX % 360.0f;
            }
            else
            {
                this.StartAngleX = this.StartAngleX % -360.0f;
            }

            this.DiffAngleX = this.DistanceAngleX - this.StartAngleX;
            if (this.DiffAngleX > 180)
            {
                this.DiffAngleX -= 360;
            }
            if (this.DiffAngleX < -180)
            {
                this.DiffAngleX += 360;
            }

            //Calculate Y: It's duplicated from calculating X since the rotation wrap should be the same
            this.StartAngleY = this.Target.RotationY;

            if (this.StartAngleY > 0)
            {
                this.StartAngleY = this.StartAngleY % 360.0f;
            }
            else
            {
                this.StartAngleY = this.StartAngleY % -360.0f;
            }

            this.DiffAngleY = this.DistanceAngleY - this.StartAngleY;
            if (this.DiffAngleY > 180)
            {
                this.DiffAngleY -= 360;
            }

            if (this.DiffAngleY < -180)
            {
                this.DiffAngleY += 360;
            }
        }

        public override void Update (float time)
        {
            if (this.Target != null)
            {
                this.Target.RotationX = this.StartAngleX + this.DiffAngleX * time;
                this.Target.RotationY = this.StartAngleY + this.DiffAngleY * time;
            }
        }

    }
}