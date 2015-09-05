namespace MetaMind.Engine.Guis.Widgets.Views.Visuals
{
    using Microsoft.Xna.Framework;
    using Services;

    public class GradientIndexedViewVisual : GradientIndexViewVisual
    {
        public GradientIndexedViewVisual(IView view) : base(view)
        {
        }

        protected override void DrawComponents(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
        }
    }
}