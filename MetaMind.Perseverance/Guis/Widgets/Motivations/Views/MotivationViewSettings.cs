using MetaMind.Engine.Guis.Widgets.Views;
using MetaMind.Engine.Settings;
using MetaMind.Perseverance.Concepts.MotivationEntries;
using MetaMind.Perseverance.Guis.Widgets.Motivations.Items;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.Motivations.Views
{
    public class MotivationViewSettings : ViewSettings1D
    {
        public Vector2         TracerMargin    = new Vector2(-new MotivationItemSettings().NameFrameSize.X / 2f, 90);
        public Point           BorderMargin    = new Point(4, 4);
        public Color           HighlightColor  = ColorPalette.TransparentColor1;

        //---------------------------------------------------------------------
        public MotivationSpace Space;
    }
}