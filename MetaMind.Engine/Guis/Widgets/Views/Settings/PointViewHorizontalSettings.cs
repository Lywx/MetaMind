namespace MetaMind.Engine.Guis.Widgets.Views.Settings
{
    using MetaMind.Engine.Guis.Widgets.Views.PointView;

    using Microsoft.Xna.Framework;

    public class PointViewHorizontalSettings : ViewSettings
    {
        public int ColumnNumDisplay = 10;

        public int ColumnNumMax     = 500;


        public PointViewHorizontalDirection Direction = PointViewHorizontalDirection.Normal;


        public Point PointMargin = new Point(50, 0);

        public Point PointStart;

        public PointViewHorizontalSettings(Point start)
        {
            this.PointStart = start;
        }

        public PointViewHorizontalSettings(Point start, Point margin, PointViewHorizontalDirection direction)
        {
            this.PointStart  = start;
            this.PointMargin = margin;

            this.Direction   = direction;
        }
    }
}