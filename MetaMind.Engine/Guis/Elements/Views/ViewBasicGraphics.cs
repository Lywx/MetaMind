namespace MetaMind.Engine.Guis.Elements.Views
{
    using System;

    using MetaMind.Engine.Guis.Elements.Items;

    using Microsoft.Xna.Framework;

    public class ViewBasicGraphics : ViewComponent, IViewGraphics
    {
        public ViewBasicGraphics( IView view, ICloneable viewSettings, ICloneable itemSettings )
            : base( view, viewSettings, itemSettings )
        {
        }

        public virtual void Update( GameTime gameTime )
        {
        }

        public virtual void Draw(GameTime gameTime, byte alpha)
        {
            foreach ( var item in this.View.Items )
            {
                if ( item.IsEnabled( ItemState.Item_Active ) )
                    item.Draw( gameTime, alpha );
            }
        }
    }
}