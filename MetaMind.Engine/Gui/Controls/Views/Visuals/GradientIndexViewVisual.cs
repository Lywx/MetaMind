namespace MetaMind.Engine.Gui.Controls.Views.Visuals
{
    using Microsoft.Xna.Framework;
    using Service;

    public class GradientIndexViewVisual : GradientViewVisual
    {
        public GradientIndexViewVisual(IView view) : base(view)
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