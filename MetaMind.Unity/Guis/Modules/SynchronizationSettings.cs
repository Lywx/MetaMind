namespace MetaMind.Unity.Guis.Modules
{
    using System;
    using Engine;
    using Engine.Component.Font;
    using Engine.Component.Graphics;
    using Engine.Setting.Color;
    using Engine.Setting.Loader;
    using Microsoft.Xna.Framework;

    public class SynchronizationSettings : GameVisualEntity, IParameterLoader<GraphicsSettings>, ICloneable
    {
        public Vector2 BarFrameCenterPosition;

        public Vector2 BarFrameSize = new Vector2(500, 8);

        public Color   BarFrameColor = new Color(30, 30, 40, 10);

        public Color   BarFrameAscendColor = Palette.LightBlue;

        public Color   BarFrameDescendColor = Palette.LightPink;

        public Color   SynchronizationPointFrameColor = Palette.Transparent20;

        public Font    SynchronizationRateFont = Font.UiStatistics;

        public Vector2 SynchronizationDotFrameSize = new Vector2(8, 8);

        private int viewportHeight;

        private int viewportWidth;

        public SynchronizationSettings()
        {
            this.LoadParameter(this.Graphics.Settings);

            this.BarFrameCenterPosition = new Vector2((float)this.viewportWidth / 2, 16);
        }

        #region Parameters

        public void LoadParameter(GraphicsSettings parameter)
        {
            this.viewportWidth  = parameter.Width;
            this.viewportHeight = parameter.Height;
        }

        #endregion

        #region IClonable

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }
}