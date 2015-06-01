namespace MetaMind.Engine.Guis.Widgets.Views.Settings
{
    using Microsoft.Xna.Framework;

    public class PointViewHorizontalSettings : ViewSettings
    {
        public int ColumnNumDisplay = 10;

        public int ColumnNumMax     = 500;

        public ViewDirection Direction = ViewDirection.Normal;


        public Vector2 PointMargin = new Vector2(50, 0);

        public Vector2 PointStart;

        public PointViewHorizontalSettings(Vector2 start)
        {
            this.PointStart = start;
        }

        public PointViewHorizontalSettings(Vector2 start, Vector2 margin, ViewDirection direction)
        {
            this.PointStart  = start;
            this.PointMargin = margin;

            this.Direction   = direction;
        }
    }
}