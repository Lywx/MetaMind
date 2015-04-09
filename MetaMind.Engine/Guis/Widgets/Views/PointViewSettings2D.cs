namespace MetaMind.Engine.Guis.Widgets.Views
{
    using MetaMind.Engine.Components.Fonts;

    using Microsoft.Xna.Framework;

    public class PointViewSettings2D : ViewSettings
    {
        public Font  NameFont   = Font.UiStatisticsFont;
        public Color NameColor  = Color.White;
        public float NameSize   = 1.5f;
        public Point NameMargin = new Point(47, 47);

        //---------------------------------------------------------------------
        public int ColumnNumDisplay = 3;
        public int ColumnNumMax     = 5;

        public int RowNumDisplay    = 3;
        public int RowNumMax        = 500;

        //---------------------------------------------------------------------
        public Point PointStart;
        public Point PointMargin = new Point(251, 150);

        public PointViewSettings2D(Point start)
        {
            this.PointStart = start;
        }

        public PointViewSettings2D(Point start, Point margin)
        {
            this.PointStart  = start;
            this.PointMargin = margin;
        }
    }
}