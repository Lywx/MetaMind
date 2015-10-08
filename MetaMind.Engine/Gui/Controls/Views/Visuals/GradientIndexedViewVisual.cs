namespace MetaMind.Engine.Gui.Controls.Views.Visuals
{
    using Microsoft.Xna.Framework;
    using Services;

    public class GradientIndexedViewVisual : GradientIndexViewVisual
    {
        public GradientIndexedViewVisual(IMMViewNode view) : base(view)
        {
        }

        protected override void DrawComponents(IMMEngineGraphicsService graphics, GameTime time, byte alpha)
        {
        }
    }
}