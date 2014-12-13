namespace MetaMind.Perseverance.Guis.Widgets
{
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Settings;

    using Microsoft.Xna.Framework;

    public class TaskViewSettings : ViewSettings2D
    {
        public readonly ViewScrollBarSettings ScrollBarSettings = new ViewScrollBarSettings();

        public Point BorderMargin      = new Point(4, 4);

        public Color HighlightColor    = ColorPalette.TransparentColor1;
    }
}