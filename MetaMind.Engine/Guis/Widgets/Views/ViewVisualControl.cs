namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class ViewVisualControl : ViewVisualComponent, IViewVisualControl
    {
        public ViewVisualControl(IView view, ICloneable viewSettings, ICloneable itemSettings)
            : base(view, viewSettings, itemSettings)
        {
        }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.DrawItems(graphics, time, alpha);
        }

        protected void DrawItems(IGameGraphicsService graphics, GameTime gameTime, byte alpha)
        {
            foreach (var item in View.Items.ToArray())
            {
                // TODO: Possible separation of active and inactive storage and looping 
                //       to improve cpu performace

                // item could be null when diposed
                if (item != null && 
                    item[ItemState.Item_Is_Active]())
                {
                    item.Draw(graphics, gameTime, alpha);
                }
            }
        }
    }
}