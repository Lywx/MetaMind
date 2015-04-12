namespace MetaMind.Perseverance.Guis.Modules
{
    using MetaMind.Engine;
    using MetaMind.Engine.Components.Fonts;

    using Microsoft.Xna.Framework;

    public class SummaryModuleSettings
    {
        public Vector2 TitleCenter         = new Vector2(GameEngine.GraphicsSettings.Width / 2f, 100);

        public Font    TitleFont           = Font.UiRegular;

        public float   TitleSize           = 1f;

        //---------------------------------------------------------------------
        public Font  EntityFont          = Font.UiStatistics;

        public float EntitySize          = 1f;

        //---------------------------------------------------------------------
        public int   LineHeight          = 25;

        //---------------------------------------------------------------------
        public int   GoodPrefessionHour  = 10;

        public int   LoftyProfessionHour = 6;

        public int   WorldRecordHour     = 110;
    }
}