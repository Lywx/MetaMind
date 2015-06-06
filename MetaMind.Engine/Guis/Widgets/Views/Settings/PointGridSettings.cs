namespace MetaMind.Engine.Guis.Widgets.Views.Settings
{
    using MetaMind.Engine.Guis.Widgets.Views.Visuals;
    using MetaMind.Engine.Settings.Colors;

    using Microsoft.Xna.Framework;

    public class PointGridSettings : PointView2DSettings
    {
        public readonly ViewScrollbarSettings ScrollbarSettings = new ViewScrollbarSettings();

        public Point BorderMargin = new Point(4, 4);

        public Color HighlightColor = Palette.Transparent1;

        #region Constructors 


        #endregion

        public PointGridSettings(
            Vector2 viewPosition,
            Vector2 itemMargin,
            int viewColumnDisplay,
            int viewColumnMax,
            int viewRowDisplay,
            int viewRowMax,
            ViewDirection viewDirection = ViewDirection.Normal)
            : base(itemMargin, viewPosition, viewColumnDisplay, viewColumnMax, viewRowDisplay, viewRowMax, viewDirection)
        {
        }
    }
}