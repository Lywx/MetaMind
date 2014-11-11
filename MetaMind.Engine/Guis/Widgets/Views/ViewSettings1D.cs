using MetaMind.Engine.Settings;
using Microsoft.Xna.Framework;
using System;

namespace MetaMind.Engine.Guis.Widgets.Views
{
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
            return MemberwiseClone();
        }
    }
}