using MetaMind.Engine.Components;

namespace MetaMind.Perseverance.Guis.Modules
{
    using MetaMind.Engine.Components.Fonts;

    public class SummaryModuleSettings
    {
        //---------------------------------------------------------------------
        public Font  TitleFont           = Font.UiRegularFont;
        public float TitleSize           = 1f;
        //---------------------------------------------------------------------
        public Font  EntityFont          = Font.UiStatisticsFont;
        public float EntitySize          = 1f;
        //---------------------------------------------------------------------
        public int   LineHeight          = 25;
        //---------------------------------------------------------------------
        public int   GoodPrefessionHour  = 8;
        public int   LoftyProfessionHour = 4;
        //---------------------------------------------------------------------
    }
}