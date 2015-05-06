namespace MetaMind.Engine.Guis.Widgets.Views.Settings
{
    using Microsoft.Xna.Framework;

    public class PointView2DSettings : PointViewHorizontalSettings
    {
        public int ColumnNumDisplay = 3;

        public int ColumnNumMax = 5;

        public int RowNumDisplay = 3;

        public int RowNumMax = 500;

        public PointView2DSettings(Vector2 start)
            : base(start)
        {
            this.PointStart = start;
            this.PointMargin = new Vector2(250, 150);
        }

        public PointView2DSettings(Vector2 start, Vector2 margin)
            : base(start)
        {
            this.PointStart  = start;
            this.PointMargin = margin;
        }
    }
}