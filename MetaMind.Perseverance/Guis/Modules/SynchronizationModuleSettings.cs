namespace MetaMind.Perseverance.Guis.Modules
{
    using System;

    using MetaMind.Engine;
    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Components.Graphics;
    using MetaMind.Engine.Settings.Colors;
    using MetaMind.Engine.Settings.Loaders;

    using Microsoft.Xna.Framework;

    public class SynchronizationModuleSettings : GameVisualEntity, IParameterLoader<GraphicsSettings>, ICloneable
    {
        //---------------------------------------------------------------------
        public int   BarFrameXC;

        public int   BarFrameYC              = 16;

        public Point BarFrameSize            = new Point(500, 8);

        public Color BarFrameBackgroundColor = new Color(30, 30, 40, 10);

        //---------------------------------------------------------------------
        public Point StateMargin             = new Point(0, 1);

        public Point StatusMargin            = new Point(0, 34);

        //---------------------------------------------------------------------
        public Font  AccumulationFont        = Font.UiStatistics;

        public Point AccumulationMargin      = new Point(170, 0);

        //---------------------------------------------------------------------
        public Point AccelerationMargin      = new Point(170, 0);

        //---------------------------------------------------------------------
        public Color SynchronizationDotFrameColor = Palette.TransparentColor1;

        public Font  SynchronizationRateFont      = Font.UiStatistics;

        public float SynchronizationRateSize      = 2.0f;

        public Color SynchronizationRateColor     = Color.White;

        public Point SynchronizationRateMargin    = new Point(210, 0);

        public SynchronizationModuleSettings()
        {
            this.LoadParameter(this.GameGraphics.Settings);

            this.BarFrameXC = this.ScreenWidth / 2;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #region Parameters

        public void LoadParameter(GraphicsSettings parameter)
        {
            this.ScreenWidth  = parameter.Width;
            this.ScreenHeight = parameter.Height;
        }

        public int ScreenHeight { get; private set; }

        public int ScreenWidth { get; private set; }

        #endregion
    }
}