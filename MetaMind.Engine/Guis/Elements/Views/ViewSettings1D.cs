namespace MetaMind.Engine.Guis.Elements.Views
{
    using System;

    using MetaMind.Engine.Settings;

    using Microsoft.Xna.Framework;

    public class ViewSettings1D : ICloneable
    {
        //---------------------------------------------------------------------
        public Point           StartPoint          = new Point(160, GraphicsSettings.Height / 2);
        public Point           RootMargin          = new Point(51, 0);

        //---------------------------------------------------------------------
        public int             ColumnNumDisplay    = 10;

        //---------------------------------------------------------------------
        public ScrollDirection Direction           = ScrollDirection.Right;

        public enum ScrollDirection
        {
            Left, Right
        }
        

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}