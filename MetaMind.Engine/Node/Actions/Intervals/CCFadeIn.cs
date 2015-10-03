namespace CocosSharp
{
    using MetaMind.Engine.Node;
    using MetaMind.Engine.Node.Actions;

    public class MMFadeIn : MMFiniteTimeAction
    {
        #region Constructors

        public MMFadeIn (float durataion) : base (durataion)
        {
        }

        #endregion Constructors


        protected internal override MMActionState StartAction(MMNode target)
        {
            return new MMFadeInState (this, target);

        }

        public override MMFiniteTimeAction Reverse ()
        {
            return new MMFadeOut (Duration);
        }
    }

    public class MMFadeInState : MMFiniteTimeActionState
    {

        protected uint Times { get; set; }

        protected bool OriginalState { get; set; }

        public MMFadeInState (MMFadeIn action, MMNode target)
            : base (action, target)
        {
        }

        public override void Update (float time)
        {
            var pRGBAProtocol = Target;
            if (pRGBAProtocol != null)
            {
                pRGBAProtocol.Opacity = (byte)(255 * time);
            }
        }
    }

}