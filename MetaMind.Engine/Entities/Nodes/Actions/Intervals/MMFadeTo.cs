namespace MetaMind.Engine.Entities.Nodes.Actions.Intervals
{
    using System;

    public class MMFadeToState : MMFiniteTimeActionState
    {
        #region Constructors

        public MMFadeToState(MMFadeTo action, IMMNode target)
            : base(action, target)
        {
            this.OpacityTo = action.OpacityTo;
            this.OpacityFrom = this.Target.Opacity.Standard;
        }

        #endregion

        protected byte OpacityFrom { get; set; }

        protected byte OpacityTo { get; set; }

        public override void Update(float time)
        {
            if (this.Target != null)
            {
                var discrepancy = this.OpacityTo - this.OpacityFrom;

                this.Target.Opacity.Standard = (byte)(this.OpacityFrom + discrepancy * time);
            }
        }
    }

    public class MMFadeTo : MMFiniteTimeAction
    {
        #region Constructors

        public MMFadeTo(float duration, byte opacity) : base(duration)
        {
            this.OpacityTo = opacity;
        }

        #endregion Constructors

        public byte OpacityTo { get; private set; }

        protected internal override MMActionState StartAction(IMMNode target)
        {
            return new MMFadeToState(this, target);
        }

        public override MMFiniteTimeAction Reverse()
        {
            throw new NotImplementedException();
        }
    }
}