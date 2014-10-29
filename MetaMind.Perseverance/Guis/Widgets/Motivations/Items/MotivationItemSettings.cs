using MetaMind.Engine.Guis.Widgets.Items;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.Motivations.Items
{
    public class MotivationItemSettings : ItemSettings
    {
        public Color WishColor = new Color( 255, 255, 255, 128 );
        public Color FearColor = new Color( 51, 204, 204, 223 );
        
        public float SymbolFrameIncrementFactor = 0.2f;
    }
}