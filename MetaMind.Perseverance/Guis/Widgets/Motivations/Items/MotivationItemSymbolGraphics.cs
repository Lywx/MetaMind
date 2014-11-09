using System;
using MetaMind.Engine.Guis.Elements.Frames;
using MetaMind.Engine.Guis.Widgets.Items;
using MetaMind.Engine.Guis.Widgets.ViewItems;
using MetaMind.Engine.Settings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MetaMind.Perseverance.Guis.Widgets.Motivations.Items
{
    public class MotivationItemSymbolGraphics : ViewItemComponent
    {
        private readonly Texture2D symbolTexture;
        private          float     rotation;
        public MotivationItemSymbolGraphics( IViewItem item )
            : base( item )
        {
            symbolTexture = ContentManager.Load<Texture2D>( @"Textures\UIs\Heart" );
        }

        private Vector2 SymbolOrigin
        {
            get { return new Vector2( symbolTexture.Width / 2f, symbolTexture.Height / 2f ); }
        }

        public void Draw( GameTime gameTime )
        {
            if ( !Item.IsEnabled( ItemState.Item_Active ) )
            {
                return;
            }
            if ( Item.IsEnabled( ItemState.Item_Dragging ) )
            {
                DrawShadow();
            }
            DrawHeart();
            DrawTracer( gameTime );
        }
        public void Update( GameTime gameTime )
        {
            UpdateRotation();
        }

        private void DrawHeart()
        {
            var flipped     = Math.Cos( rotation ) > 0;
            var size        = new Point( ( int ) ( Math.Abs( Math.Cos( rotation ) ) * ( ( IPickableFrame ) ItemControl.SymbolFrame ).Rectangle.Width ), ( ( IPickableFrame ) ItemControl.SymbolFrame ).Rectangle.Height );
            var destination = ( ( IPickableFrame ) ItemControl.SymbolFrame ).DestinationWithSize( size );

            if ( ItemData.Property == "Neutral" )
                ScreenManager.SpriteBatch.Draw( symbolTexture, destination, null, Item.IsEnabled( ItemState.Item_Selected ) ? ColorPalette.TransparentColor5 : ColorPalette.TransparentColor3, 0f, SymbolOrigin, flipped ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f );
            else if ( ItemData.Property == "Wish" )
                ScreenManager.SpriteBatch.Draw( symbolTexture, destination, null, ItemSettings.WishColor, 0f, SymbolOrigin, flipped ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f );
            else if ( ItemData.Property == "Fear" )
                ScreenManager.SpriteBatch.Draw( symbolTexture, destination, null, ItemSettings.FearColor, 0f, SymbolOrigin, flipped ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f );
        }

        private void DrawShadow()
        {
            var scrollCenter = ViewControl.Scroll.RootCenterPoint( ItemControl.Id );
            var destination  = new Rectangle( scrollCenter.X, scrollCenter.Y, ItemControl.RootFrame.Rectangle.Width, ItemControl.RootFrame.Rectangle.Height );

            ScreenManager.SpriteBatch.Draw( symbolTexture, destination, null, ColorPalette.TransparentColor3, 0f, SymbolOrigin, SpriteEffects.None, 0f );
        }

        private void DrawTracer( GameTime gameTime )
        {
            if ( ItemControl.Tracer != null && Item.IsEnabled( ItemState.Item_Selected ) )
            {
                ItemControl.Tracer.Draw( gameTime, ( byte) ( rotation / MathHelper.Pi * 255 ) );
            }
        }
        private void UpdateRotation()
        {
            if ( Item.IsEnabled( ItemState.Item_Selected ) )
            {
                rotation += MathHelper.ToRadians( 5 );
            }
            else
            {
                if ( rotation > MathHelper.ToRadians( 360 ) )
                {
                    rotation -= MathHelper.ToRadians( 365 );
                }
                else if ( rotation > MathHelper.ToRadians( 0 ) )
                {
                    rotation -= MathHelper.ToRadians( 5 );
                }
            }
        }
    }
}