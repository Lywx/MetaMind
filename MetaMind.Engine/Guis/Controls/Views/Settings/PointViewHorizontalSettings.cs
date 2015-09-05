namespace MetaMind.Engine.Guis.Controls.Views.Settings
{
    using Microsoft.Xna.Framework;

    public class PointViewHorizontalSettings : PointViewSettings, IPointViewHorizontalSettings
    {
        public PointViewHorizontalSettings(Vector2 itemMargin, Vector2 viewPosition, int viewColumnNumDisplay, int viewColumnMax, ViewDirection viewDirection = ViewDirection.Normal)
            : base(itemMargin, viewPosition, viewDirection)
        {
            this.ViewColumnDisplay = viewColumnNumDisplay;
            this.ViewColumnMax     = viewColumnMax;
        }

        public int ViewColumnDisplay { get; set; }

        public int ViewColumnMax { get; set; }
    }
}