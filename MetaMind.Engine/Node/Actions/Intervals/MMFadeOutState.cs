namespace CocosSharp
{
    using MetaMind.Engine.Node;
    using MetaMind.Engine.Node.Actions;

    public class MMFadeOutState : MMFiniteTimeActionState
    {
        public MMFadeOutState(MMFadeOut action, MMNode target)
            : base(action, target) {}

        public override void Update(float time)
        {
            if (this.Target != null)
            {
                this.Target.Opacity.Standalone = (byte)(255 * (1 - time));
            }
        }
    }
}