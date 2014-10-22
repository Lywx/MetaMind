using System;
using MetaMind.Engine.Components;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Settings
{
    public class WindowSettings : ICloneable
    {
        //---------------------------------------------------------------------
        // name display
        public Font  NameFont = Font.UiStatisticsFont;
        public Color NameColor = Color.White;
        public float NameSize = 1.5f;
        public Point NameMargin = new Point(47, 47);

        //---------------------------------------------------------------------
        public Color CurrentColor  = new Color( 0, 20, 50, 200 );
        public Color ReceiverColor = new Color( 255, 20, 0, 200 );

        //---------------------------------------------------------------------
        public byte  AppearRate                     = 24;
        public byte  DisappearRate                  = 48;

        //---------------------------------------------------------------------
        public Point StartPoint = new Point( 160, GraphicsSettings.Height / 2 );
        public Point BorderMargin = new Point(10, 10);

        //---------------------------------------------------------------------
        public static WindowSettings Default
        {
            get { return new WindowSettings(); }
        }

        //---------------------------------------------------------------------
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}