using System;
using MetaMind.Engine.Components;
using MetaMind.Engine.Guis.Widgets.Views;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Settings
{
    public class ViewSettings1D : ICloneable
    {
        //---------------------------------------------------------------------
        // name display
        public Font  NameFont    = Font.UiStatisticsFont;
        public Color NameColor   = Color.White;
        public float NameSize    = 1.5f;
        public Point NameMargin  = new Point( 47, 47 );

        //---------------------------------------------------------------------
        // scroll
        public Point StartPoint = new Point( 160, GraphicsSettings.Height / 2 );
        public Point RootMargin = new Point( 251, 0 );

        //---------------------------------------------------------------------
        public int ColumnNumDisplay = 3;

        //---------------------------------------------------------------------
        public static ViewSettings1D Default { get { return new ViewSettings1D(); } }

        //---------------------------------------------------------------------
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}