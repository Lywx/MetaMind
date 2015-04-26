namespace MetaMind.Engine.Guis.Widgets.Views
{
    using MetaMind.Engine.Settings.Colors;

    using Microsoft.Xna.Framework;

    public class PointListSettings : PointViewSettings1D
    {
        public Point BorderMargin = new Point(4, 4);

        public Color HighlightColor = Palette.TransparentColor1;

        public PointListSettings(Point start, Point margin, PointViewDirection direction)
            : base(start, margin, direction)
        {
        }

        public PointListSettings(Point start)
            : base(start)
        {
        }
    }
}