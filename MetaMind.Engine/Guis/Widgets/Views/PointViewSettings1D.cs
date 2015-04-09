namespace MetaMind.Engine.Guis.Widgets.Views
{
    using Microsoft.Xna.Framework;

    public class PointViewSettings1D : ViewSettings
    {
        //---------------------------------------------------------------------
        public int ColumnNumDisplay = 10;
        public int ColumnNumMax     = 500;

        //---------------------------------------------------------------------
        public ScrollDirection Direction = ScrollDirection.Right;

        //---------------------------------------------------------------------
        public Point PointMargin = new Point(51, 0);
        public Point PointStart;

        public PointViewSettings1D(Point start)
        {
            this.PointStart = start;
        }

        public PointViewSettings1D(Point start, Point margin, ScrollDirection direction)
        {
            this.PointStart  = start;
            this.PointMargin = margin;

            this.Direction   = direction;
        }

        public enum ScrollDirection
        {
            Left, Right
        }
    }
}