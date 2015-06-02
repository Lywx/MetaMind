namespace MetaMind.Engine.Guis.Widgets.Views.Settings
{
    using Microsoft.Xna.Framework;

    public class PointView2DSettings : PointViewHorizontalSettings
    {
        public int RowNumDisplay;

        public int RowNumMax;

        public PointView2DSettings(
            Vector2 position,
            Vector2 margin,
            int columnNumDisplay,
            int columnNumMax,
            int rowNumDisplay,
            int rowNumMax,
            ViewDirection direction = ViewDirection.Normal)
            : base(position, margin, columnNumDisplay, columnNumMax, direction)
        {
            this.RowNumDisplay = rowNumDisplay;
            this.RowNumMax     = rowNumMax;
        }
    }
}