namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Items;

    using Microsoft.Xna.Framework;

    public class ViewGraphics : ViewVisualComponent, IViewGraphics
    {
        public ViewGraphics(IView view, ICloneable viewSettings, ICloneable itemSettings)
            : base(view, viewSettings, itemSettings)
        {
        }

        public override void Draw(IGameGraphics gameGraphics, GameTime gameTime, byte alpha)
        {
            this.DrawItems(gameGraphics, gameTime, alpha);
        }

        protected void DrawItems(IGameGraphics gameGraphics, GameTime gameTime, byte alpha)
        {
            foreach (var item in View.Items.ToArray())
            {
                // TODO: Possible separation of active and inactive storage and looping 
                //       to improve cpu performace

                // item could be null when diposed
                if (item != null && 
                    item.IsEnabled(ItemState.Item_Active))
                {
                    item.Draw(gameGraphics, gameTime, alpha);
                }
            }
        }
    }
}