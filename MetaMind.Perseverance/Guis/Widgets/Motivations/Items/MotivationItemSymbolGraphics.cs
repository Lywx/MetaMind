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
        private float     rotation;
        private Texture2D heartTexture;

        public MotivationItemSymbolGraphics( IViewItem item )
            : base( item )
        {
            heartTexture = ContentManager.Load<Texture2D>( "Textures/UIs/Heart" );
        }

        public void Draw( GameTime gameTime )
        {
            if ( !Item.IsEnabled( ItemState.Item_Active ) )
                return;

            var heartOrigin = new Vector2( heartTexture.Width / 2f, heartTexture.Height / 2f );
            DrawShadow( heartOrigin );
            DrawHeart( heartOrigin );
        }

        public void Update( GameTime gameTime )
        {
            UpdateRotation();
        }

        private void DrawHeart( Vector2 origin )
        {
            var flipped     = Math.Cos( rotation ) > 0;
            var size        = new Point( ( int ) ( Math.Abs( Math.Cos( rotation ) ) * ( ( IPickableFrame ) ItemControl.SymbolFrame ).Rectangle.Width ), ( ( IPickableFrame ) ItemControl.SymbolFrame ).Rectangle.Height );
            var destination = ( ( IPickableFrame ) ItemControl.SymbolFrame ).DestinationWithSize( size );

            if ( ItemData.Property == "Neutral" )
                ScreenManager.SpriteBatch.Draw( heartTexture, destination, null, Item.IsEnabled( ItemState.Item_Selected ) ? ColorPalette.TransparentColor5 : ColorPalette.TransparentColor3, 0f, origin, flipped ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f );
            else if ( ItemData.Property == "Wish" )
                ScreenManager.SpriteBatch.Draw( heartTexture, destination, null, ItemSettings.WishColor, 0f, origin, flipped ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f );
            else if ( ItemData.Property == "Fear" )
                ScreenManager.SpriteBatch.Draw( heartTexture, destination, null, ItemSettings.FearColor, 0f, origin, flipped ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f );
        }

        private void DrawShadow( Vector2 origin )
        {
            var scrollCenter = ViewControl.Scroll.RootCenterPoint( ItemControl.Id );
            var destination = new Rectangle( scrollCenter.X, scrollCenter.Y, ItemControl.RootFrame.Rectangle.Width, ItemControl.RootFrame.Rectangle.Height );

            ScreenManager.SpriteBatch.Draw( heartTexture, destination, null, ColorPalette.TransparentColor3, 0f, origin, SpriteEffects.None, 0f );
        }

        private void UpdateRotation()
        {
            if ( Item.IsEnabled( ItemState.Item_Selected ) )
                rotation += MathHelper.ToRadians( 5 );
            else
            {
                if ( rotation > MathHelper.ToRadians( 360 ) )
                    rotation -= MathHelper.ToRadians( 365 );
                else if ( rotation > MathHelper.ToRadians( 0 ) )
                    rotation -= MathHelper.ToRadians( 5 );
            }
        }
    }
}