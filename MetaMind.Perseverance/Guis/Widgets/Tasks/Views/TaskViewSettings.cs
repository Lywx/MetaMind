using MetaMind.Engine.Settings;
using MetaMind.Perseverance.Guis.Widgets.Tasks.Items;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.Tasks.Views
{
    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Guis.Elements.Views;

    public class TaskViewSettings : ViewSettings2D
    {
        public Font  NotificationFont  = Font.UiStatisticsFont;

        public float NotificationSize  = 0.7f;

        public Color NotificationColor = Color.White;

        public Color HighlightColor    = ColorPalette.TransparentColor1;

        public Point BorderMargin      = new Point(4, 4);

        public ViewScrollBarSettings ScrollBarSettings = new ViewScrollBarSettings();

        public TaskViewSettings()
        {
            var itemSettings = new TaskItemSettings();

            RootMargin = new Point(itemSettings.NameFrameSize.X, itemSettings.NameFrameSize.Y + itemSettings.IdFrameSize.Y);
        }
    }
}