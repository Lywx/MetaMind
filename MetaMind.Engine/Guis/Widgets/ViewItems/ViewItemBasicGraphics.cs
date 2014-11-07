using System.Globalization;
using C3.Primtive2DXna;
using MetaMind.Engine.Guis.Widgets.Items;
using Microsoft.Xna.Framework;

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

            if ( Item.IsEnabled( ItemState.Item_Selected ) )
            {
                Primitives2D.FillRectangle( ScreenManager.SpriteBatch, ItemControl.RootFrame.Rectangle, ItemSettings.RootFrameColor );
                Primitives2D.FillRectangle( ScreenManager.SpriteBatch, ItemControl.NameFrame.Rectangle, ItemSettings.NameFrameHighlightColor );
            }
            else if ( Item.IsEnabled( ItemState.Item_Editing ) )
            {
                Primitives2D.FillRectangle( ScreenManager.SpriteBatch, ItemControl.RootFrame.Rectangle, ItemSettings.RootFrameColor );
                Primitives2D.FillRectangle( ScreenManager.SpriteBatch, ItemControl.NameFrame.Rectangle, ItemSettings.NameFrameModificationColor );
            }
            else
            {
                Primitives2D.FillRectangle( ScreenManager.SpriteBatch, ItemControl.RootFrame.Rectangle, ItemSettings.RootFrameColor );
                Primitives2D.FillRectangle( ScreenManager.SpriteBatch, ItemControl.NameFrame.Rectangle, ItemSettings.NameFrameRegularColor );
            }
            DrawId();
        }

        protected void DrawId()
        {
            FontManager.DrawCenteredText( ItemSettings.IdFont, ItemControl.Id.ToString( new CultureInfo( "en-US" ) ), IdCenter, ItemSettings.IdColor, ItemSettings.IdSize );
        }

        protected Point Center
        {
            get { return ItemControl.RootFrame.Center; }
        }
        
        protected virtual Vector2 IdCenter
        {
            get { return new Vector2( ItemControl.RootFrame.Rectangle.Right + 10, ItemControl.RootFrame.Rectangle.Top - 10 ); }
        }

        public virtual void Update( GameTime gameTime )
        {
        }
    }
}