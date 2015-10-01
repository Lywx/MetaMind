// --------------------------------------------------------------------------------------------------------------------
// <copyright file="">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
namespace MetaMind.Engine.Node.Actions.Intervals
{
    public class MMScaleTo : MMFiniteTimeAction
    {
        #region Constructors

        public MMScaleTo (float duration, float scale) : this (duration, scale, scale)
        {
        }

        public MMScaleTo (float duration, float scaleX, float scaleY) : base (duration)
        {
            this.EndScaleX = scaleX;
            this.EndScaleY = scaleY;
        }

        #endregion Constructors

        public float EndScaleX { get; private set; }

        public float EndScaleY { get; private set; }

        public override MMFiniteTimeAction Reverse()
        {
            throw new System.NotImplementedException ();
        }

        protected internal override MMActionState StartAction(MMNode target)
        {
            return new MMScaleToState (this, target);
        }
    }
}