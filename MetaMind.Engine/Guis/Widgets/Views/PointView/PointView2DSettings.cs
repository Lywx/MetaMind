namespace MetaMind.Engine.Guis.Widgets.Views.PointView
{
    using MetaMind.Engine.Components.Fonts;

    using Microsoft.Xna.Framework;

    public class PointView2DSettings : ViewSettings
    {
        public Font  NameFont   = Font.UiStatistics;

        public Color NameColor  = Color.White;

        public float NameSize   = 1.5f;

        public Point NameMargin = new Point(47, 47);


        public int   ColumnNumDisplay = 3;

        public int   ColumnNumMax     = 5;

        public int   RowNumDisplay    = 3;

        public int   RowNumMax        = 500;


        public Point PointStart;

        public Point PointMargin = new Point(250, 150);

        public PointView2DSettings(Point start)
        {
            this.PointStart = start;
        }

        public PointView2DSettings(Point start, Point margin)
        {
            this.PointStart  = start;
            this.PointMargin = margin;
        }
    }
}