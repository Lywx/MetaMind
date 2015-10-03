namespace CocosSharp
{
    using MetaMind.Engine.Node;
    using MetaMind.Engine.Node.Actions;

    public class MMFadeOut : MMFiniteTimeAction
    {
        #region Constructors

        public MMFadeOut(float durtaion) : base(durtaion) {}

        #endregion Constructors

        protected internal override MMActionState StartAction(MMNode target)
        {
            return new MMFadeOutState(this, target);
        }

        public override MMFiniteTimeAction Reverse()
        {
            return new MMFadeIn(this.Duration);
        }
    }
}
