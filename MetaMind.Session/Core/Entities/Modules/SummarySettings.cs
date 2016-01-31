namespace MetaMind.Session.Guis.Modules
{
    using Engine.Core.Backend.Content.Fonts;
    using Engine.Core.Backend.Graphics;
    using Engine.Core.Entity.Common;
    using Engine.Core.Settings;
    using Microsoft.Xna.Framework;

    public class SummarySettings : MMVisualEntity, IMMParameterDependant<MMGraphicsSettings>
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

        public MMFont    TitleFont = MMFont.UiRegular;

        public float   TitleSize = 1f;

        public Color   TitleColor = Color.White;

        public MMFont  EntityFont = MMFont.UiStatistics;

        public float EntitySize = 1f;

        public int LineHeight = 25;

        public int HourOfGood = 10;

        public int HourOfLofty = 6;

        public int HourOfWorldRecord = 110;

        #endregion

        public SummarySettings()
        {
            this.LoadParameter(this.EngineGraphics.Settings);

            this.TitleCenter = new Vector2(this.ViewportWidth / 2f, 100);
        }
    }
}