using System.Collections.Generic;
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

        public override void Draw( GameTime gameTime )
        {
            DrawSymbol( gameTime );
            DrawName();
            DrawId();
        }


        private void DrawSymbol( GameTime gameTime )
        {
            symbol.Draw( gameTime );
        }

        private void DrawName()
        {
            if (!Item.IsEnabled(ItemState.Item_Selected))
                return;
            
            List<string> text = FontManager.BreakTextIntoList( ItemSettings.NameFont, ItemSettings.NameSize, ItemData.Name, ( float ) ViewSettings.RootMargin.X * 4 );
            for ( var i = 0 ; i < text.Count ; i++ )
            {
                FontManager.DrawText( ItemSettings.NameFont, text[ i ], NamePosition + new Vector2( 0, ItemSettings.NameLineMargin ) * i, ItemSettings.NameColor, ItemSettings.NameSize );
            }
        }

        private Vector2 NamePosition
        {
            get { return ( ( Rectangle ) ItemControl.RootFrame.Rectangle ).Center.ToVector2() + new Vector2( 0, 50 ); }
        }

        public override void Update( GameTime gameTime )
        {
            symbol.Update( gameTime );
        }
    }
}