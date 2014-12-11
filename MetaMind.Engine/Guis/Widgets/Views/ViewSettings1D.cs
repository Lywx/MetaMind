namespace MetaMind.Engine.Guis.Widgets.Views
{
    using MetaMind.Engine.Settings;

    using Microsoft.Xna.Framework;

    public class ViewSettings1D : ViewSettings
    {
        //---------------------------------------------------------------------
        public Point           StartPoint          = new Point(160, GraphicsSettings.Height / 2);
        public Point           RootMargin          = new Point(51, 0);

        //---------------------------------------------------------------------
        public int             ColumnNumDisplay    = 10;
        public int             ColumnNumMax        = 500;

        //---------------------------------------------------------------------
        public ScrollDirection Direction           = ScrollDirection.Right;

        public enum ScrollDirection
        {
            Left, Right
        }
    }
}