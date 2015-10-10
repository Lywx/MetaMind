namespace MetaMind.Engine.Gui.Controls.Views.Visuals
{
    using System;
    using Microsoft.Xna.Framework;
    using Services;

    public class FadeViewVisual : GradientViewVisual
    {
        public FadeViewVisual(IMMViewNode view) : base(view)
        {
        }

        public override void Draw(GameTime time)
        {
            this.DrawItems(graphics, time, Math.Min((byte)this.FocusOpacity, alpha));
            this.DrawComponents(graphics, time, Math.Min((byte)this.FocusOpacity, alpha));
        }
    }
}