namespace MetaMind.Engine.Guis.Widgets.Views.Settings
{
    using MetaMind.Engine.Guis.Widgets.Views.PointView;

    using Microsoft.Xna.Framework;

    public class PointViewHorizontalSettings : ViewSettings
    {
        public int ColumnNumDisplay = 10;

        public int ColumnNumMax     = 500;

        public PointViewDirection Direction = PointViewDirection.Normal;


        public Vector2 PointMargin = new Vector2(50, 0);

        public Vector2 PointStart;

        public PointViewHorizontalSettings(Vector2 start)
        {
            this.PointStart = start;
        }

        public PointViewHorizontalSettings(Vector2 start, Vector2 margin, PointViewDirection direction)
        {
            this.PointStart  = start;
            this.PointMargin = margin;

            this.Direction   = direction;
        }
    }
}