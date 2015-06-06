namespace MetaMind.Engine.Guis.Widgets.Views.Settings
{
    using Microsoft.Xna.Framework;

    public class PointView2DSettings : PointViewHorizontalSettings, IPointViewVerticalSettings, IPointViewHorizontalSettings
    {
        public PointView2DSettings(Vector2 itemMargin, Vector2 viewPosition, int viewColumnDisplay, int viewColumnMax, int viewRowDisplay, int viewRowMax, ViewDirection viewDirection = ViewDirection.Normal)
            : base(itemMargin, viewPosition, viewColumnDisplay, viewColumnMax, viewDirection)
        {
            this.ViewRowDisplay = viewRowDisplay;
            this.ViewRowMax     = viewRowMax;
        }

        public int ViewRowDisplay { get; set; }
        public int ViewRowMax { get; set; }
    }
}