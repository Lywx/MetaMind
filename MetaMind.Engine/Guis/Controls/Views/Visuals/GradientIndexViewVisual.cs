namespace MetaMind.Engine.Guis.Controls.Views.Visuals
{
    using Microsoft.Xna.Framework;
    using Services;

    public class GradientIndexViewVisual : GradientViewVisual
    {
        public GradientIndexViewVisual(IView view) : base(view)
        {
        }

        protected override void DrawItems(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            foreach (var item in this.View.ItemsRead.ToArray())
            {
                item.Draw(graphics, time, alpha);
            }
        }
    }
}