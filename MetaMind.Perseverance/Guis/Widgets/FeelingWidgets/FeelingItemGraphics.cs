using System.Globalization;
using MetaMind.Engine.Guis.Widgets.ViewItems;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.FeelingWidgets
{
    public class FeelingItemGraphics : ViewItemBasicGraphics
    {
        private FeelingItemSymbolGraphics symbol;
        public FeelingItemGraphics(IViewItem item) : base(item)
        {
            symbol = new FeelingItemSymbolGraphics( item );
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