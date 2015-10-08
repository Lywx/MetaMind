namespace MetaMind.Engine.Nodes.Actions.Intervals
{
    public class MMFadeInState : MMFiniteTimeActionState
    {
        public MMFadeInState(MMFadeIn action, IMMNode target)
            : base(action, target) {}

        public override void Update(float time)
        {
            if (this.Target != null)
            {
                this.Target.Opacity.Standalone = (byte)(255 * time);
            }
        }
    }

    public class MMFadeIn : MMFiniteTimeAction
    {
        #region Constructors

        public MMFadeIn(float durataion) : base(durataion) {}

        #endregion Constructors

        protected internal override MMActionState StartAction(IMMNode target)
        {
            return new MMFadeInState(this, target);
        }

        public override MMFiniteTimeAction Reverse()
        {
            return new MMFadeOut(this.Duration);
        }
    }
}
