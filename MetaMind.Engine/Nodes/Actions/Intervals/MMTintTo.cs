namespace MetaMind.Engine.Nodes.Actions.Intervals
{
    using Microsoft.Xna.Framework;
    using System;

    public class MMTintToState : MMFiniteTimeActionState
    {
        public MMTintToState(
            MMTintTo action,
            IMMNode target,
            IMMNodeColor targetColor)
            : base(action, target)
        {
            if (targetColor == null)
            {
                throw new ArgumentNullException(nameof(targetColor));
            }

            this.TargetColor = targetColor;

            this.ColorTo   = action.ColorTo;
            this.ColorFrom = targetColor.Standalone;
        }

        protected Color ColorFrom { get; set; }

        protected Color ColorTo { get; set; }

        protected IMMNodeColor TargetColor { get; set; }

        public override void Update(float time)
        {
            this.TargetColor.Standalone =
                new Color(
                    (byte)(this.ColorFrom.R + (this.ColorTo.R - this.ColorFrom.R) * time),
                    (byte)(this.ColorFrom.G + (this.ColorTo.G - this.ColorFrom.G) * time),
                    (byte)(this.ColorFrom.B + (this.ColorTo.B - this.ColorFrom.B) * time));
        }
    }

    public class MMTintTo : MMFiniteTimeAction
    {
        #region Constructors

        public MMTintTo(float duration, byte red, byte green, byte blue)
            : base(duration)
        {
            this.ColorTo = new Color(red, green, blue);
        }

        #endregion Constructors

        public Color ColorTo { get; }

        public override MMFiniteTimeAction Reverse()
        {
            throw new NotImplementedException();
        }

        protected internal override MMActionState StartAction(IMMNode target)
        {
            return new MMTintToState(this, target);
        }
    }
}