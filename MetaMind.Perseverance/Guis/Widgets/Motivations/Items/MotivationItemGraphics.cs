using System.Collections.Generic;
using System.Globalization;
using MetaMind.Engine.Components;
using MetaMind.Engine.Extensions;
using MetaMind.Engine.Guis.Widgets.Items;
using MetaMind.Engine.Guis.Widgets.ViewItems;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.Motivations.Items
{
    public class MotivationItemGraphics : ViewItemBasicGraphics
    {
        private readonly MotivationItemSymbolGraphics symbol;

        public MotivationItemGraphics( IViewItem item )
            : base( item )
        {
            symbol = new MotivationItemSymbolGraphics( item );
        }

        private Vector2 NamePosition
        {
            get { return ( ( Rectangle ) ItemControl.RootFrame.Rectangle ).Center.ToVector2() + new Vector2( 0, 50 ); }
        }

        public override void Draw(GameTime gameTime, byte alpha)
        {
            DrawSymbol( gameTime, alpha );
            DrawName( alpha );
            DrawId( alpha );
        }


        public override void Update( GameTime gameTime )
        {
            symbol.Update( gameTime );
        }

        protected override void DrawId( byte alpha )
        {
            FontManager.DrawCenteredText( ItemSettings.IdFont, ItemControl.Id.ToString( new CultureInfo( "en-US" ) ), IdCenter, Item.IsEnabled( ItemState.Item_Pending ) ? ItemSettings.IdPendingColor : ItemSettings.IdColor, ItemSettings.IdSize );
        }

        private void DrawName( byte alpha )
        {
            if ( !Item.IsEnabled( ItemState.Item_Selected ) )
                return;

            List<string> text = FontManager.BreakTextIntoList( ItemSettings.NameFont, ItemSettings.NameSize, ItemData.Name, ( float ) ViewSettings.RootMargin.X * 4 );
            for ( var i = 0 ; i < text.Count ; i++ )
            {
                FontManager.DrawCenteredText( ItemSettings.NameFont, text[ i ], NamePosition + new Vector2( 0, ItemSettings.NameLineMargin ) * i, ColorExt.MakeTransparent( ItemSettings.NameColor, alpha ), ItemSettings.NameSize );
            }
        }

        private void DrawSymbol(GameTime gameTime, byte alpha)
        {
            symbol.Draw( gameTime, alpha );
        }
    }
}