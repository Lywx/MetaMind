namespace MetaMind.Engine.Core.Entity.Control.Views.Renderers
{
    using System;
    using Microsoft.Xna.Framework;

    public class FadeViewVisual : GradientViewVisual
    {
        public FadeViewVisual(IMMView view) : base(view)
        {
        }

        public override void Draw(GameTime time)
        {
            this.DrawItems(graphics, time, Math.Min((byte)this.FocusOpacity, alpha));
            this.DrawComponents(graphics, time, Math.Min((byte)this.FocusOpacity, alpha));
        }
    }
}