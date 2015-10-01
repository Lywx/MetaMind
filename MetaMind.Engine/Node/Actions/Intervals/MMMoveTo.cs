// --------------------------------------------------------------------------------------------------------------------
// <copyright file="">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>

namespace MetaMind.Engine.Node.Actions.Intervals
{
    using Geometry;

    public class MMMoveTo : MMMoveBy
    {
        #region Constructors

        public MMMoveTo(float duration, MMPoint location) : base(duration, location)
        {
            this.EndLocation = location;
        }

        #endregion Constructors

        public MMPoint EndLocation { get; private set; }

        #region Operations

        protected internal override MMActionState StartAction(MMNode target)
        {
            return new MMMoveToState(this, target);
        }

        #endregion
    }
}
