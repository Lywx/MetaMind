namespace MetaMind.Session.Guis.Modules
{
    using System;
    using Engine.Components.Graphics;
    using Engine.Entities;
    using Engine.Entities.Bases;
    using Engine.Services.IO;
    using Engine.Settings;
    using Microsoft.Xna.Framework;

    public class SynchronizationSettings : MMVisualEntity, IMMParameterDependant<MMGraphicsSettings>, ICloneable
    {
        public Vector2 BarFrameCenterPosition;

        public Vector2 BarFrameSize = new Vector2(500, 8);

        public Color   BarFrameColor = new Color(30, 30, 40, 10);

        public Color   BarFrameAscendColor = MMPalette.LightBlue;

        public Color   BarFrameDescendColor = MMPalette.LightPink;

        public Color   SynchronizationPointFrameColor = MMPalette.Transparent20;

        public Font    SynchronizationRateFont = Font.UiStatistics;

        public Vector2 SynchronizationDotFrameSize = new Vector2(8, 8);

        private int viewportHeight;

        private int viewportWidth;

        public SynchronizationSettings()
        {
            this.LoadParameter(this.EngineGraphics.Settings);

            this.BarFrameCenterPosition = new Vector2((float)this.viewportWidth / 2, 16);
        }

        #region Parameters

        public void LoadParameter(MMGraphicsSettings parameter)
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