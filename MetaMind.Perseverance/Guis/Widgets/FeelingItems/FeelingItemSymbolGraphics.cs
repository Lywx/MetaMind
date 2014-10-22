using System;
using MetaMind.Engine.Guis.Elements.Frames;
using MetaMind.Engine.Guis.Widgets.Items;
using MetaMind.Engine.Guis.Widgets.ViewItems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MetaMind.Perseverance.Guis.Widgets.FeelingItems
{
    public class FeelingItemSymbolGraphics : ViewItemComponent
    {
        private Random    random = new Random();
        private float     rotation;
        private Texture2D heartTexture;

        public FeelingItemSymbolGraphics( IViewItem item )
            : base( item )
        {
            LoadTextures();
        }

        public void Draw( GameTime gameTime )
        {
            var spriteBatch = ScreenManager.SpriteBatch;
            var heartOrigin = new Vector2( heartTexture.Width / 2f, heartTexture.Height / 2f );

            //------------------------------------------------------------------
            // preprocessing what to draw
            if ( !Item.IsEnabled( ItemState.Item_Active ) )
            {
                return;
            }
            else
            {
                DrawShadow( spriteBatch, heartOrigin );
                DrawHeart( spriteBatch, heartOrigin );
            }
        }

        public void Update( GameTime gameTime )
        {
            UpdateRotation();
        }


        private void DrawHeart( SpriteBatch spriteBatch, Vector2 origin )
        {
            IPickableFrame symbolFrame = ItemControl.SymbolFrame;

            var flipped = Math.Cos( rotation ) > 0;
            var size = new Point( ( int ) ( Math.Abs( Math.Cos( rotation ) ) * symbolFrame.Rectangle.Width ), symbolFrame.Rectangle.Height );
            var destination =  symbolFrame.DestinationWithSize( size );
            switch ( ( ( FeelingItemControl ) ItemControl ).Type )
            {
                case FeelingType.Neutral:
                    {
                        if ( Item.IsEnabled( ItemState.Item_Selected ) )
                            spriteBatch.Draw( heartTexture, destination, null, ItemSettings.TransparentColor5, 0f, origin, flipped ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f );
                        else
                            spriteBatch.Draw( heartTexture, destination, null, ItemSettings.TransparentColor3, 0f, origin, flipped ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f );

                        break;
                    }

                case FeelingType.Wish:
                    spriteBatch.Draw( heartTexture, destination, null, ItemSettings.WishColor, 0f, origin, flipped ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f );
                    break;

                case FeelingType.Fear:
                    spriteBatch.Draw( heartTexture, destination, null, ItemSettings.FearColor, 0f, origin, flipped ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f );
                    break;
            }
        }

        private void DrawShadow( SpriteBatch spriteBatch, Vector2 origin )
        {
            var scrollCenter = Item.ViewControl.Scroll.RootCenterPoint( Item.ItemControl.Id );
            var destination = new Rectangle( scrollCenter.X, scrollCenter.Y, ItemControl.RootFrame.Rectangle.Width, ItemControl.RootFrame.Rectangle.Height );
            spriteBatch.Draw( heartTexture, destination, null, ItemSettings.TransparentColor3, 0f, origin, SpriteEffects.None, 0f );
        }

        private void LoadTextures()
        {
            heartTexture = ContentManager.Load<Texture2D>( "Textures/UIs/Heart" );
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