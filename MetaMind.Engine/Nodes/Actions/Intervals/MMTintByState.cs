namespace MetaMind.Engine.Nodes.Actions.Intervals
{
    using System;
    using Microsoft.Xna.Framework;

    public class MMTintByState : MMFiniteTimeActionState
    {
        public MMTintByState(MMTintBy action, IMMNode target, IMMNodeColor targetColor)
            : base(action, target)
        {
            if (targetColor == null)
            {
                throw new ArgumentNullException(nameof(targetColor));
            }

            this.TargetColor = targetColor;

            this.BDelta = action.BDelta;
            this.GDelta = action.GDelta;
            this.RDelta = action.RDelta;

            this.RFrom = targetColor.Standalone.R;
            this.GFrom = targetColor.Standalone.G;
            this.BFrom = targetColor.Standalone.B;
        }

        protected IMMNodeColor TargetColor { get; set; }

        protected short BDelta { get; set; }

        protected short GDelta { get; set; }

        protected short RDelta { get; set; }

        protected short BFrom { get; set; }

        protected short GFrom { get; set; }

        protected short RFrom { get; set; }

        public override void Update(float time)
        {
            this.TargetColor.Standalone = new Color(
                (byte)(this.RFrom + this.RDelta * time),
                (byte)(this.GFrom + this.GDelta * time),
                (byte)(this.BFrom + this.BDelta * time));
        }
    }
}