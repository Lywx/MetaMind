namespace MetaMind.Engine.Guis.Widgets.Views.Settings
{
    using MetaMind.Engine.Guis.Widgets.Views.PointView;
    using MetaMind.Engine.Settings.Colors;

    using Microsoft.Xna.Framework;

    public class PointListSettings : PointViewHorizontalSettings
    {
        public Point BorderMargin = new Point(4, 4);

        public Color HighlightColor = Palette.TransparentColor1;

        public PointListSettings(Vector2 start, Vector2 margin, PointViewDirection direction)
            : base(start, margin, direction)
        {
        }

        public PointListSettings(Vector2 start)
            : base(start)
        {
        }
    }
}