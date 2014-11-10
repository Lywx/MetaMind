using MetaMind.Engine.Guis.Widgets.Views;
using MetaMind.Perseverance.Concepts.MotivationEntries;
using MetaMind.Perseverance.Guis.Widgets.Motivations.Items;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.Motivations.Views
{
    public class MotivationViewSettings : ViewSettings1D
    {
        public Vector2         TracerMargin = new Vector2( -new MotivationItemSettings().NameFrameSize.X / 2f, 90 );
        public MotivationSpace Space;
    }
}