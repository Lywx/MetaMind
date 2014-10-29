using System;
using MetaMind.Engine.Settings;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Widgets.Views
{
    public class ViewSettings1D : ICloneable
    {
        public enum ScrollDirection
        {
            Left,
            Right
        }
        
        //---------------------------------------------------------------------
        public Point StartPoint          = new Point( 160, GraphicsSettings.Height / 2 );
        public Point RootMargin          = new Point( 51, 0 );
        //---------------------------------------------------------------------
        public int   ColumnNumDisplay    = 10;
        //---------------------------------------------------------------------
        public ScrollDirection Direction = ScrollDirection.Right;


        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}