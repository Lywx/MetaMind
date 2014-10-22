﻿using System.Globalization;
using MetaMind.Engine.Guis.Widgets.Items;
using Microsoft.Xna.Framework;
using C3.Primtive2DXna;

namespace MetaMind.Engine.Guis.Widgets.ViewItems
{
    public class ViewItemBasicGraphics : ViewItemComponent, IItemGraphics
    {
        public ViewItemBasicGraphics( IViewItem item )
            : base( item )
        {
        }

        public virtual void Draw( GameTime gameTime )
        {
            if ( !Item.IsEnabled( ItemState.Item_Active ) && !Item.IsEnabled( ItemState.Item_Dragging ) )
                return;

            var spriteBatch = ScreenManager.SpriteBatch;
            if ( Item.IsEnabled( ItemState.Item_Selected ) )
            {
                Primitives2D.FillRectangle( spriteBatch, ItemControl.RootFrame.Rectangle, ItemSettings.RootFrameColor );
                Primitives2D.FillRectangle( spriteBatch, ItemControl.NameFrame.Rectangle, ItemSettings.NameFrameHighlightColor );
            }
            else if ( Item.IsEnabled( ItemState.Item_Editing ) )
            {
                Primitives2D.FillRectangle( spriteBatch, ItemControl.RootFrame.Rectangle, ItemSettings.RootFrameColor );
                Primitives2D.FillRectangle( spriteBatch, ItemControl.NameFrame.Rectangle, ItemSettings.NameFrameModificationColor );
            }
            else
            {
                Primitives2D.FillRectangle( spriteBatch, ItemControl.RootFrame.Rectangle, ItemSettings.RootFrameColor );
                Primitives2D.FillRectangle( spriteBatch, ItemControl.NameFrame.Rectangle, ItemSettings.NameFrameRegularColor );
            }
            // id
            FontManager.DrawCenteredText( ItemSettings.IdFont, ItemControl.Id.ToString( new CultureInfo( "en-US" ) ), IdPosition, ItemSettings.IdColor, ItemSettings.IdSize );
        }

        protected Vector2 IdPosition
        {
            get { return new Vector2( ItemControl.RootFrame.Rectangle.Right + 10, ItemControl.RootFrame.Rectangle.Top - 10 ); }
        }

        public virtual void Update( GameTime gameTime )
        {
        }
    }
}