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

        public override void Draw(GameTime gameTime, byte alpha)
        {
            this.DrawItems(gameTime, alpha);
        }

        protected void DrawItems(GameTime gameTime, byte alpha)
        {
            foreach (var item in View.Items.ToArray())
            {
                // item could be null when diposed
                if (item != null && 
                    item.IsEnabled(ItemState.Item_Active))
                {
                    item.Draw(gameTime, alpha);
                }
            }
        }
    }
}