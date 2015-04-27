namespace MetaMind.Engine.Guis.Widgets.Views
{
    using Microsoft.Xna.Framework;

    public class PointViewSettings1D : ViewSettings
    {
        public int ColumnNumDisplay = 10;

        public int ColumnNumMax     = 500;

        public PointViewDirection Direction = PointViewDirection.Normal;

        public Point PointMargin = new Point(50, 0);

        public Point PointStart;

        public PointViewSettings1D(Point start)
        {
            this.PointStart = start;
        }

        public PointViewSettings1D(Point start, Point margin, PointViewDirection direction)
        {
            this.PointStart  = start;
            this.PointMargin = margin;

            this.Direction   = direction;
        }
    }
}