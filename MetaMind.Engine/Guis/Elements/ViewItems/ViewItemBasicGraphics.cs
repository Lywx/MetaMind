namespace MetaMind.Engine.Guis.Elements.ViewItems
{
    using System.Globalization;

    using C3.Primtive2DXna;

    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Guis.Elements.Items;

    using Microsoft.Xna.Framework;

    public class ViewItemBasicGraphics : ViewItemComponent, IItemGraphics
    {
        public ViewItemBasicGraphics( IViewItem item )
            : base( item )
        {
        }

        public virtual void Draw( GameTime gameTime, byte alpha )
        {
            if ( !this.Item.IsEnabled( ItemState.Item_Active ) && !this.Item.IsEnabled( ItemState.Item_Dragging ) )
                return;

            if ( this.Item.IsEnabled( ItemState.Item_Selected ) )
            {
                Primitives2D.FillRectangle( this.ScreenManager.SpriteBatch, this.ItemControl.RootFrame.Rectangle, this.ItemSettings.RootFrameColor );
                Primitives2D.FillRectangle( this.ScreenManager.SpriteBatch, this.ItemControl.NameFrame.Rectangle, this.ItemSettings.NameFrameHighlightColor );
            }
            else if ( this.Item.IsEnabled( ItemState.Item_Editing ) )
            {
                Primitives2D.FillRectangle( this.ScreenManager.SpriteBatch, this.ItemControl.RootFrame.Rectangle, this.ItemSettings.RootFrameColor );
                Primitives2D.FillRectangle( this.ScreenManager.SpriteBatch, this.ItemControl.NameFrame.Rectangle, this.ItemSettings.NameFrameModificationColor );
            }
            else
            {
                Primitives2D.FillRectangle( this.ScreenManager.SpriteBatch, this.ItemControl.RootFrame.Rectangle, this.ItemSettings.RootFrameColor );
                Primitives2D.FillRectangle( this.ScreenManager.SpriteBatch, this.ItemControl.NameFrame.Rectangle, this.ItemSettings.NameFrameRegularColor );
            }
            this.DrawId( alpha );
        }

        protected virtual void DrawId( byte alpha )
        {
            this.FontManager.DrawCenteredText( this.ItemSettings.IdFont, this.ItemControl.Id.ToString( new CultureInfo( "en-US" ) ), this.IdCenter, ColorExt.MakeTransparent( this.ItemSettings.IdColor, alpha ), this.ItemSettings.IdSize );
        }

        protected Point Center
        {
            get { return this.ItemControl.RootFrame.Center; }
        }
        
        protected virtual Vector2 IdCenter
        {
            get { return new Vector2( this.ItemControl.RootFrame.Rectangle.Right + 10, this.ItemControl.RootFrame.Rectangle.Top - 10 ); }
        }

        public virtual void Update( GameTime gameTime )
        {
        }
    }
}