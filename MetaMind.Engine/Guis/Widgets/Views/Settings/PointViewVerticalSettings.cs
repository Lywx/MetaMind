namespace MetaMind.Engine.Guis.Widgets.Views.Settings
{
    using Microsoft.Xna.Framework;

    public class PointViewVerticalSettings : PointViewSettings
    {
        public int RowNumDisplay;

        public int RowNumMax;

        public PointViewVerticalSettings(
            Vector2 position,
            Vector2 margin,
            int rowNumDisplay,
            int rowNumMax,
            ViewDirection direction = ViewDirection.Normal)
            : base(position, margin, direction)
        {
            this.RowNumDisplay = rowNumDisplay;
            this.RowNumMax     = rowNumMax;
        }
    }
}