namespace MetaMind.Engine.Guis.Widgets.Views.Settings
{
    using Microsoft.Xna.Framework;

    public class PointViewHorizontalSettings : PointViewSettings
    {
        public int ColumnNumDisplay;

        public int ColumnNumMax;

        public PointViewHorizontalSettings(
            Vector2 position,
            Vector2 margin,
            int columnNumDisplay,
            int columnNumMax,
            ViewDirection direction = ViewDirection.Normal)
            : base(position, margin, direction)
        {
            this.ColumnNumDisplay = columnNumDisplay;
            this.ColumnNumMax     = columnNumMax;
        }
    }
}