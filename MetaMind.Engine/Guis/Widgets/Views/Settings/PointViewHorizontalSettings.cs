namespace MetaMind.Engine.Guis.Widgets.Views.Settings
{
    using Microsoft.Xna.Framework;

    public class PointViewHorizontalSettings : ViewSettings
    {
        public Vector2 Margin;

        public Vector2 Position;

        public int ColumnNumDisplay;

        public int ColumnNumMax;

        public ViewDirection Direction;

        public PointViewHorizontalSettings(
            Vector2 position,
            Vector2 margin,
            int columnNumDisplay,
            int columnNumMax,
            ViewDirection direction = ViewDirection.Normal)
        {
            this.Position  = position;
            this.Margin = margin;

            this.ColumnNumDisplay = columnNumDisplay;
            this.ColumnNumMax     = columnNumMax;

            this.Direction = direction;
        }
    }
}