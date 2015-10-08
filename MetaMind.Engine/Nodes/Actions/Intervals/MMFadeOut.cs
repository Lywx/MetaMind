namespace MetaMind.Engine.Nodes.Actions.Intervals
{
    public class MMFadeOutState : MMFiniteTimeActionState
    {
        public MMFadeOutState(MMFadeOut action, IMMNode target)
            : base(action, target) {}

        public override void Update(float time)
        {
            if (this.Target != null)
            {
                this.Target.Opacity.Standalone = (byte)(255 * (1 - time));
            }
        }
    }

    public class MMFadeOut : MMFiniteTimeAction
    {
        #region Constructors

        public MMFadeOut(float durtaion) : base(durtaion) {}

        #endregion Constructors

        protected internal override MMActionState StartAction(IMMNode target)
        {
            return new MMFadeOutState(this, target);
        }

        public override MMFiniteTimeAction Reverse()
        {
            return new MMFadeIn(this.Duration);
        }
    }
}
