namespace CocosSharp
{
    using MetaMind.Engine.Node;
    using MetaMind.Engine.Node.Actions;

    public class MMBlink : MMFiniteTimeAction
    {
        #region Constructors

        public MMBlink(float duration, uint numOfBlinks) : base(duration)
        {
            this.Times = numOfBlinks;
        }

        #endregion Constructors

        public uint Times { get; }


        protected internal override MMActionState StartAction(MMNode target)
        {
            return new MMBlinkState(this, target);
        }

        public override MMFiniteTimeAction Reverse()
        {
            return new MMBlink(this.Duration, this.Times);
        }
    }
}
