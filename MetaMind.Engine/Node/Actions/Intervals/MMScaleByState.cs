// --------------------------------------------------------------------------------------------------------------------
// <copyright file="">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
namespace MetaMind.Engine.Node.Actions.Intervals
{
    public class MMScaleByState : MMScaleToState
    {

        public MMScaleByState (MMScaleTo action, MMNode target)
            : base (action, target)
        { 
            this.DeltaX = this.StartScaleX * this.EndScaleX - this.StartScaleX;
            this.DeltaY = this.StartScaleY * this.EndScaleY - this.StartScaleY;
        }

    }
}