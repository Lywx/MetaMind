using MetaMind.Engine.Settings;
using MetaMind.Perseverance.Guis.Widgets.Tasks.Items;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.Tasks.Views
{
    using MetaMind.Engine.Guis.Elements.Views;

    public class TaskViewSettings : ViewSettings2D
    {
        //---------------------------------------------------------------------
        public Color                     HighlightColor    = ColorPalette.TransparentColor1;

        //---------------------------------------------------------------------
        public Point                     BorderMargin      = new Point(4, 4);

        //---------------------------------------------------------------------
        public TaskViewScrollBarSettings ScrollBarSettings = new TaskViewScrollBarSettings();

        public TaskViewSettings()
        {
            var itemSettings = new TaskItemSettings();
            
            RootMargin = new Point( itemSettings.NameFrameSize.X , itemSettings.NameFrameSize.Y + itemSettings.IdFrameSize.Y);
        }
    }
}