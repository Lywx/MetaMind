namespace MetaMind.Unity.Guis.Modules
{
    using System;
    using Engine;
    using Engine.Components.Fonts;
    using Engine.Components.Graphics;
    using Engine.Settings.Colors;
    using Engine.Settings.Loaders;
    using Microsoft.Xna.Framework;

    public class SynchronizationSettings : GameVisualEntity, IParameterLoader<GraphicsSettings>, ICloneable
    {
        public Vector2 BarFrameCenterPosition;

        public Vector2 BarFrameSize = new Vector2(500, 8);

        public Color   BarFrameColor = new Color(30, 30, 40, 10);

        public Color   BarFrameAscendColor = Palette.LightBlue;

        public Color   BarFrameDescendColor = Palette.LightPink;

        public Color   SynchronizationPointFrameColor = Palette.Transparent1;

        public Font    SynchronizationRateFont = Font.UiStatistics;

        public Vector2 SynchronizationDotFrameSize = new Vector2(8, 8);

        public SynchronizationSettings()
        {
            this.LoadParameter(this.GameGraphics.Settings);

            this.BarFrameCenterPosition = new Vector2((float)this.ScreenWidth / 2, 16);
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