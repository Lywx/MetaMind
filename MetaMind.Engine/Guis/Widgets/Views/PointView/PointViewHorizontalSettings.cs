namespace MetaMind.Engine.Guis.Widgets.Views.PointView
{
    using Microsoft.Xna.Framework;

    public class PointViewHorizontalSettings : ViewSettings
    {
        public int ColumnNumDisplay = 10;

        public int ColumnNumMax     = 500;


        public PointView1DDirection Direction = PointView1DDirection.Normal;


        public Point PointMargin = new Point(50, 0);

        public Point PointStart;

        public PointViewHorizontalSettings(Point start)
        {
            this.PointStart = start;
        }

        public PointViewHorizontalSettings(Point start, Point margin, PointView1DDirection direction)
        {
            this.PointStart  = start;
            this.PointMargin = margin;

            this.Direction   = direction;
        }
    }
}