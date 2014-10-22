using System;
using MetaMind.Engine.Guis.Widgets.Items;
using MetaMind.Engine.Settings;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Widgets.Views
{
    public class ViewBasicGraphics : ViewComponent, IViewGraphics
    {
        public ViewBasicGraphics( IView view, ICloneable viewSettings, ICloneable itemSettings )
            : base( view, viewSettings, itemSettings )
        {
        }

        public void Update( GameTime gameTime )
        {
        }

        public virtual void Draw( GameTime gameTime )
        {
            foreach ( var item in View.Items )
            {
                if (item.IsEnabled(ItemState.Item_Active))
                    item.Draw( gameTime );
            }
        }
    }
}