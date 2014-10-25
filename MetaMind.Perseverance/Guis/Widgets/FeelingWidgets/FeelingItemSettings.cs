using MetaMind.Engine.Guis.Widgets.Items;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.FeelingWidgets
{
    public class FeelingItemSettings : ItemSettings
    {
        public Color WishColor    = new Color( 255, 255, 255, 128 );
        public Color FearColor    = new Color( 51, 204, 204, 255 - 32 );

        public static new FeelingItemSettings Default { get { return new FeelingItemSettings(); } }
    }
}