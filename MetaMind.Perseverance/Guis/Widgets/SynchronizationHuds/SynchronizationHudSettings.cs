using System;
using MetaMind.Engine.Components;
using MetaMind.Engine.Settings;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.SynchronizationHuds
{
    public class SynchronizationHudSettings : ICloneable
    {
        //---------------------------------------------------------------------
        public int   BarFrameXC              = GraphicsSettings.Width / 2;
        public int   BarFrameYC              = 16;
        public Point BarFrameSize            = new Point( 500, 8 );
        public Color BarFrameBackgroundColor = new Color( 30, 30, 40, 10 );
        public Color BarFrameAscendColor     = new Color( 78, 255, 27, 200 );
        public Color BarFrameDescendColor    = new Color( 255, 0, 27, 200 );

        //---------------------------------------------------------------------
        public Point StateMargin        = new Point( 0, 1 );
        public Font  StateFont          = Font.UiStatisticsFont;
        public float StateSize          = 1.1f;
        public Color StateColor   = Color.White;

        //---------------------------------------------------------------------
        public float StatusSize       = 0.7f;
        public Color StatusColor  = Color.White;

        //---------------------------------------------------------------------
        public Font  AccumulationFont   = Font.UiStatisticsFont;
        public Point AccumulationMargin = new Point( 170, 0 );
        public float AccumulationSize   = 0.7f;
        public Color AccumulationColor  = Color.White;

        //---------------------------------------------------------------------
        public Point AccelerationMargin = new Point( 170, 0 );
        public Font  AccelerationFont   = Font.UiStatisticsFont;
        public float AccelerationSize   = 2.0f;
        public Color AccelerationColor  = Color.White;
        
        //---------------------------------------------------------------------
        public Font  MessageFont      = Font.UiStatisticsFont;
        public float MessageSize      = 0.7f;

        //---------------------------------------------------------------------
        public Point InformationMargin  = new Point( 0, 34 );

        //---------------------------------------------------------------------
        public Color HourFrameColor = new Color( 10, 10, 40, 40 );

        //---------------------------------------------------------------------
        public static SynchronizationHudSettings Default { get { return new SynchronizationHudSettings(); } }
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}