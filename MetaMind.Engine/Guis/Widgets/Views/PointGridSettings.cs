namespace MetaMind.Engine.Guis.Widgets.Views
{
    using MetaMind.Engine.Settings;
    using MetaMind.Engine.Settings.Colors;

    using Microsoft.Xna.Framework;

    public class PointGridSettings : PointViewSettings2D
    {
        public readonly ViewScrollBarSettings ScrollBarSettings = new ViewScrollBarSettings();

        public Point BorderMargin = new Point(4, 4);

        public Color HighlightColor = Palette.TransparentColor1;

        #region Constructors 

        public PointGridSettings(Point start)
            : base(start)
        {
        }

        public PointGridSettings(Point start, Point margin)
            : base(start, margin)
        {
        }

        #endregion
    }
}