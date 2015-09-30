namespace MetaMind.Engine.Gui.Controls.Views.Visuals
{
    using Microsoft.Xna.Framework;
    using Service;

    public class GradientIndexedViewVisual : GradientIndexViewVisual
    {
        public GradientIndexedViewVisual(IView view) : base(view)
        {
        }

        protected override void DrawComponents(IMMEngineGraphicsService graphics, GameTime time, byte alpha)
        {
        }
    }
}