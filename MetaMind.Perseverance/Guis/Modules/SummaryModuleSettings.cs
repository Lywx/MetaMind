namespace MetaMind.Perseverance.Guis.Modules
{
    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Settings;

    using Microsoft.Xna.Framework;

    public class SummaryModuleSettings
    {
        public Vector2 TitleCenter         = new Vector2(GraphicsSettings.Width / 2f, 100);

        public Font    TitleFont           = Font.UiRegularFont;

        public float   TitleSize           = 1f;

        //---------------------------------------------------------------------
        public Font  EntityFont          = Font.UiStatisticsFont;

        public float EntitySize          = 1f;

        //---------------------------------------------------------------------
        public int   LineHeight          = 25;

        //---------------------------------------------------------------------
        public int   GoodPrefessionHour  = 10;

        public int   LoftyProfessionHour = 6;

        public int   WorldRecordHour     = 110;
    }
}