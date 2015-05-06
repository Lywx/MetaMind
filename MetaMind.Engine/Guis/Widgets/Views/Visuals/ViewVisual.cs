namespace MetaMind.Engine.Guis.Widgets.Views.Visuals
{
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class ViewVisual : ViewVisualComponent, IViewVisual
    {
        public ViewVisual(IView view)
            : base(view, "viewVisual")
        {
        }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            foreach (var item in this.View.Items.ToArray())
            {
                // TODO: Possible separation of active and inactive storage and looping to improve cpu performace

                // item could be null when diposed
                if (item[ItemState.Item_Is_Active]())
                {
                    item.Draw(graphics, time, alpha);
                }
            }
        }
    }
}