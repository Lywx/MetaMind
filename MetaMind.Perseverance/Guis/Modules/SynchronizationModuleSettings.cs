namespace MetaMind.Perseverance.Guis.Modules
{
    using System;

    using MetaMind.Engine;
    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Components.Graphics;
    using MetaMind.Engine.Settings.Colors;
    using MetaMind.Engine.Settings.Loaders;

    using Microsoft.Xna.Framework;

    public class SynchronizationModuleSettings : IParameterLoader<GraphicsSettings>, ICloneable
    {
        //---------------------------------------------------------------------
        public int   BarFrameXC;

        public int   BarFrameYC              = 16;

        public Point BarFrameSize            = new Point(500, 8);

        public Color BarFrameBackgroundColor = new Color(30, 30, 40, 10);

        public Color BarFrameAscendColor     = Palette.LightBlue;

        public Color BarFrameDescendColor    = Palette.LightPink;

        //---------------------------------------------------------------------
        public Point StateMargin             = new Point(0, 1);

        public Font  StateFont               = Font.UiStatistics;

        public float StateSize               = 1.1f;

        public Color StateColor              = Color.White;

        //---------------------------------------------------------------------
        public float StatusSize              = 0.7f;

        public Color StatusColor             = Color.White;

        public Point StatusMargin            = new Point(0, 34);

        //---------------------------------------------------------------------
        public Font  AccumulationFont        = Font.UiStatistics;

        public Point AccumulationMargin      = new Point(170, 0);

        public float AccumulationSize        = 0.7f;

        public Color AccumulationColor       = Color.White;

        //---------------------------------------------------------------------
        public Point AccelerationMargin      = new Point(170, 0);

        public Font  AccelerationFont        = Font.UiStatistics;

        public float AccelerationSize        = 2.0f;

        public Color AccelerationColor       = Color.White;

        //---------------------------------------------------------------------
        public Font  MessageFont             = Font.UiStatistics;

        public float MessageSize             = 0.7f;

        //---------------------------------------------------------------------
        public Color SynchronizationDotFrameColor = Palette.TransparentColor1;

        public Font  SynchronizationRateFont      = Font.UiStatistics;

        public float SynchronizationRateSize      = 2.0f;

        public Color SynchronizationRateColor     = Color.White;

        public Point SynchronizationRateMargin    = new Point(210, 0);

        /// ---------------------------------------------------------------------
        public int   ValveFrameX       = 5;

        public int   ValveFrameY       = 16;

        public Point ValveFrameSize    = new Point(400, 8);

        public Color ValueAscendColor  = Palette.LightBlue;

        public Color ValueDescendColor = Palette.LightPink;

        public float ValueStatusSize   = 2.0f;

        public Font  ValveStateFont    = Font.UiStatistics;

        public SynchronizationModuleSettings()
        {
            this.LoadParameter(GameEngine.GraphicsSettings);

            this.BarFrameXC = this.Width / 2;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #region Parameters

        public void LoadParameter(GraphicsSettings parameter)
        {
            this.Width = parameter.Width;

        }

        private int Width { get; set; }

        #endregion
    }
}