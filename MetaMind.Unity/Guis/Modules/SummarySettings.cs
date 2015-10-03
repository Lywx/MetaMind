namespace MetaMind.Unity.Guis.Modules
{
    using Engine;
    using Engine.Components.Content.Fonts;
    using Engine.Components.Graphics;
    using Engine.Entities;
    using Engine.Service.Loader;
    using Microsoft.Xna.Framework;

    public class SummarySettings : MMVisualEntity, IParameterLoader<MMGraphicsSettings>
    {
        #region Parameters

        public void LoadParameter(MMGraphicsSettings parameter)
        {
            this.ViewportWidth = parameter.Width;
        }

        public int ViewportWidth { get; set; }

        #endregion

        #region Detail Data

        public Vector2 TitleCenter;

        public Font    TitleFont = Font.UiRegular;

        public float   TitleSize = 1f;

        public Color   TitleColor = Color.White;

        public Font  EntityFont = Font.UiStatistics;

        public float EntitySize = 1f;

        public int LineHeight = 25;

        public int HourOfGood = 10;

        public int HourOfLofty = 6;

        public int HourOfWorldRecord = 110;

        #endregion

        public SummarySettings()
        {
            this.LoadParameter(this.Graphics.Settings);

            this.TitleCenter = new Vector2(this.ViewportWidth / 2f, 100);
        }
    }
}