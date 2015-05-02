namespace MetaMind.Engine.Guis.Widgets.Views.Settings
{
    using MetaMind.Engine.Guis.Widgets.Views.PointView;
    using MetaMind.Engine.Settings.Colors;

    using Microsoft.Xna.Framework;

    public class PointListSettings : PointViewHorizontalSettings
    {
        public Point BorderMargin = new Point(4, 4);

        public Color HighlightColor = Palette.TransparentColor1;

        public PointListSettings(Point start, Point margin, PointViewHorizontalDirection direction)
            : base(start, margin, direction)
        {
        }

        public PointListSettings(Point start)
            : base(start)
        {
        }
    }
}