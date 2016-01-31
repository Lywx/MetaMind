namespace MetaMind.Engine.Core.Entity.Control.Views.Renderers
{
    using Microsoft.Xna.Framework;
    using Services;

    public class GradientIndexViewVisual : GradientViewVisual
    {
        public GradientIndexViewVisual(IMMView view) : base(view)
        {
        }

        protected override void DrawItems(IMMEngineGraphicsService graphics, GameTime time, byte alpha)
        {
            foreach (var item in this.View.Items.ToArray())
            {
                item.Draw(graphics, time, alpha);
            }
        }
    }
}