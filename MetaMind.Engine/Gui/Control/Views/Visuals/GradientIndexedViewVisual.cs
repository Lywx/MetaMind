namespace MetaMind.Engine.Gui.Control.Views.Visuals
{
    using Microsoft.Xna.Framework;
    using Service;

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