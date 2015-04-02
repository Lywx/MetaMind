namespace MetaMind.Engine.Guis.Widgets.Views
{
    using MetaMind.Engine.Settings.Colors;

    using Microsoft.Xna.Framework;

    public class GridSettings : PointViewSettings2D
    {
        public readonly ViewScrollBarSettings ScrollBarSettings = new ViewScrollBarSettings();

        public Point BorderMargin      = new Point(4, 4);

        public Color HighlightColor    = ColorPalette.TransparentColor1;
    }
}