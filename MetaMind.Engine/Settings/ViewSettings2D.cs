using System;
using MetaMind.Engine.Components;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Settings
{
    public class ViewSettings2D : ICloneable
    {
        //---------------------------------------------------------------------
        // name display
        public Font  NameFont    = Font.UiStatisticsFont;
        public Color NameColor   = Color.White;
        public float NameSize    = 1.5f;
        public Point NameMargin  = new Point( 47, 47 );

        //---------------------------------------------------------------------
        public int ColumnNumDisplay = 3;
        public int ColumnNumMax     = 5;
        public int RowNumDisplay    = 3;
        public int RowNumMax        = 500;
        
        //---------------------------------------------------------------------
        // scroll
        public Point StartPoint = new Point( 160, GraphicsSettings.Height / 2 );
        public Point RootMargin = new Point( 251, 150 );

        //---------------------------------------------------------------------
        public static ViewSettings2D Default { get { return new ViewSettings2D(); } }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}