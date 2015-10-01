// --------------------------------------------------------------------------------------------------------------------
// <copyright file="">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
namespace MetaMind.Engine.Node.Actions.Intervals
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
}