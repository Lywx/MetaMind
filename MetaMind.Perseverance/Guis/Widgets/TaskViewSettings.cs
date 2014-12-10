namespace MetaMind.Perseverance.Guis.Widgets
{
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Settings;

    using Microsoft.Xna.Framework;

    public class TaskViewSettings : ViewSettings2D
    {
        public Color HighlightColor    = ColorPalette.TransparentColor1;

        public Point BorderMargin      = new Point(4, 4);

        public ViewScrollBarSettings ScrollBarSettings = new ViewScrollBarSettings();

        public TaskViewSettings()
        {
            var itemSettings = new TaskItemSettings();

            this.RootMargin = new Point(itemSettings.NameFrameSize.X, itemSettings.NameFrameSize.Y + itemSettings.IdFrameSize.Y);
        }
    }
}