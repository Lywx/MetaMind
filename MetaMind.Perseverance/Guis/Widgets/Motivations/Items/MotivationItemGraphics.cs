using System.Globalization;
using MetaMind.Engine.Guis.Widgets.ViewItems;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.Motivations.Items
{
    public class MotivationItemGraphics : ViewItemBasicGraphics
    {
        private MotivationItemSymbolGraphics symbol;
        public MotivationItemGraphics(IViewItem item) : base(item)
        {
            symbol = new MotivationItemSymbolGraphics( item );
        }

        public override void Draw(GameTime gameTime)
        {
            symbol.Draw( gameTime );
            FontManager.DrawCenteredText( ItemSettings.IdFont, ItemControl.Id.ToString( new CultureInfo( "en-US" ) ), IdPosition, ItemSettings.IdColor, ItemSettings.IdSize );
        }

        public override void Update(GameTime gameTime)
        {
            symbol.Update( gameTime );
        }
    }
}