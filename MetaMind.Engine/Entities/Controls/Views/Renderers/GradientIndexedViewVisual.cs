namespace MetaMind.Engine.Entities.Controls.Views.Renderers
{
    using Microsoft.Xna.Framework;
    using Services;

    public class GradientIndexedViewVisual : GradientIndexViewVisual
    {
        public GradientIndexedViewVisual(IMMView view) : base(view)
        {
        }

        protected override void DrawComponents(IMMEngineGraphicsService graphics, GameTime time, byte alpha)
        {
        }
    }
}