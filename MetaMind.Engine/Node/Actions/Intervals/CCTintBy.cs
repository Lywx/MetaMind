namespace CocosSharp
{
    using MetaMind.Engine.Node;
    using MetaMind.Engine.Node.Actions;
    using Microsoft.Xna.Framework;

    public class MMTintBy : MMFiniteTimeAction
    {
        #region Constructors

        public MMTintBy(
            float duration,
            short deltaRed,
            short deltaGreen,
            short deltaBlue) : base(duration)
        {
            this.DeltaR = deltaRed;
            this.DeltaG = deltaGreen;
            this.DeltaB = deltaBlue;
        }

        #endregion Constructors

        public short DeltaB { get; }

        public short DeltaG { get; }

        public short DeltaR { get; }


        protected internal override MMActionState StartAction(MMNode target)
        {
            return new MMTintByState(this, target);
        }

        public override MMFiniteTimeAction Reverse()
        {
            return new MMTintBy(
                this.Duration,
                (short)-this.DeltaR,
                (short)-this.DeltaG,
                (short)-this.DeltaB);
        }
    }


    public class MMTintByState : MMFiniteTimeActionState
    {
        public MMTintByState(MMTintBy action, MMNode target)
            : base(action, target)
        {
            this.DeltaB = action.DeltaB;
            this.DeltaG = action.DeltaG;
            this.DeltaR = action.DeltaR;

            var protocol = target;
            if (protocol != null)
            {
                var color = protocol.Color;
                this.FromR = color.R;
                this.FromG = color.G;
                this.FromB = color.B;
            }
        }

        protected short DeltaB { get; set; }

        protected short DeltaG { get; set; }

        protected short DeltaR { get; set; }

        protected short FromB { get; set; }

        protected short FromG { get; set; }

        protected short FromR { get; set; }

        public override void Update(float time)
        {
            if (this.Target != null)
            {
                this.Target.Color. =
                    new Color(
                        (byte)(this.FromR + this.DeltaR * time),
                        (byte)(this.FromG + this.DeltaG * time),
                        (byte)(this.FromB + this.DeltaB * time));
            }
        }
    }
}
