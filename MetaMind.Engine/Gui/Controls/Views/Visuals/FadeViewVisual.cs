namespace MetaMind.Engine.Gui.Controls.Views.Visuals
{
    using System;
    using Microsoft.Xna.Framework;
    using Service;

    public class FadeViewVisual : GradientViewVisual
    {
        public FadeViewVisual(IView view) : base(view)
        {
        }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.DrawItems(graphics, time, Math.Min((byte)this.FocusAlpha, alpha));
            this.DrawComponents(graphics, time, Math.Min((byte)this.FocusAlpha, alpha));
        }
    }
}