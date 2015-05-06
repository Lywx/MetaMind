namespace MetaMind.Engine.Guis.Widgets.Views.Settings
{
    using MetaMind.Engine.Guis.Widgets.Views.Visuals;
    using MetaMind.Engine.Settings.Colors;

    using Microsoft.Xna.Framework;

    public class PointGridSettings : PointView2DSettings
    {
        public readonly ViewScrollbarSettings ScrollbarSettings = new ViewScrollbarSettings();

        public Point BorderMargin = new Point(4, 4);

        public Color HighlightColor = Palette.TransparentColor1;

        #region Constructors 

        public PointGridSettings(Vector2 start)
            : base(start)
        {
        }

        public PointGridSettings(Vector2 start, Vector2 margin)
            : base(start, margin)
        {
        }

        #endregion
    }
}