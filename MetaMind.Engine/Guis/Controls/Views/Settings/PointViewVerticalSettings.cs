namespace MetaMind.Engine.Guis.Controls.Views.Settings
{
    using Microsoft.Xna.Framework;

    public class PointViewVerticalSettings : PointViewSettings, IPointViewVerticalSettings
    {
        public int ViewRowDisplay { get; set; }

        public int ViewRowMax { get; set; }

        public PointViewVerticalSettings(Vector2 itemMargin, Vector2 viewPosition, int viewRowDisplay, int viewRowMax, ViewDirection viewDirection = ViewDirection.Normal)
            : base(itemMargin, viewPosition, viewDirection)
        {
            this.ViewRowDisplay = viewRowDisplay;
            this.ViewRowMax     = viewRowMax;
        }
    }
}