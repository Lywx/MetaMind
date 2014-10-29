using System.Globalization;
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
            symbol.Draw( gameTime );

                   DrawId();
        }
        
        public override void Update( GameTime gameTime )
        {
            symbol.Update( gameTime );
        }
    }
}