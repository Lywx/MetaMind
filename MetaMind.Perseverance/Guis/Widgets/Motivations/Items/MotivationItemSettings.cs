using MetaMind.Engine.Components;
using MetaMind.Engine.Guis.Widgets.Items;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.Motivations.Items
{
    public class MotivationItemSettings : ItemSettings
    {
        //---------------------------------------------------------------------
        public float NameSize                      = 0.6f;
        public Color NameColor                     = Color.White;
        public Font  NameFont                      = Font.InfoSimSunFont;
        public Point NameFrameSize                 = new Point( 256, 34 );
        public int   NameLineMargin                = 20;
        //---------------------------------------------------------------------
        public Color WishColor                     = new Color( 255, 255, 255, 128 );
        public Color FearColor                     = new Color( 51, 204, 204, 223 );
        //---------------------------------------------------------------------
        public float SymbolFrameIncrementFactor    = 0.2f;
    }
}