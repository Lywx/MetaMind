// --------------------------------------------------------------------------------------------------------------------
// <copyright file="">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
namespace MetaMind.Engine.Node.Actions.Intervals
{
    public class MMMoveToState : MMMoveByState
    {

        public MMMoveToState(MMMoveTo action, MMNode target)
            : base(action, target)
        {
            this.StartLocation = target.Location;
            this.DeltaLocation = action.EndLocation- target.Location;
        }

        public override void Update(float time)
        {
            if (this.Target != null)
            {
                var newLocation = this.StartLocation + this.DeltaLocation * time;

                this.Target.Location = newLocation;
                this.PreviousLocation = newLocation;
            }
        }
    }
}