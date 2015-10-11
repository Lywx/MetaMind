namespace MetaMind.Engine.Entities.Controls.Views.Visuals
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
            foreach (var item in this.View.ItemsRead.ToArray())
            {
                item.Draw(graphics, time, alpha);
            }
        }
    }
}