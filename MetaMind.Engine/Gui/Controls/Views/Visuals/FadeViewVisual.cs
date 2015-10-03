namespace MetaMind.Engine.Gui.Controls.Views.Visuals
{
    using System;
    using Microsoft.Xna.Framework;
    using Service;

    public class FadeViewVisual : GradientViewVisual
    {
        public FadeViewVisual(IMMViewNode view) : base(view)
        {
        }

        public override void Draw(IMMEngineGraphicsService graphics, GameTime time, byte alpha)
        {
            this.DrawItems(graphics, time, Math.Min((byte)this.FocusOpacity, alpha));
            this.DrawComponents(graphics, time, Math.Min((byte)this.FocusOpacity, alpha));
        }
    }
}