namespace MetaMind.Perseverance.Guis.Modules
{
    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Components.Graphics;
    using MetaMind.Engine.Settings.Loaders;

    using Microsoft.Xna.Framework;

    public class SummaryModuleSettings : IParameterLoader<GraphicsSettings>
    {
        #region Parameters

        public void LoadParameter(GraphicsSettings parameter)
        {
            this.ScreenWidth = parameter.Width;
        }

        private int ScreenWidth { get; set; }

        #endregion
        
        public Vector2 TitleCenter;

        public Font    TitleFont         = Font.UiRegular;

        public float   TitleSize         = 1f;

        //---------------------------------------------------------------------
        public Font  EntityFont          = Font.UiStatistics;

        public float EntitySize          = 1f;

        //---------------------------------------------------------------------
        public int   LineHeight          = 25;

        //---------------------------------------------------------------------
        public int   GoodPrefessionHour  = 10;

        public int   LoftyProfessionHour = 6;

        public int   WorldRecordHour     = 110;

        public SummaryModuleSettings(GraphicsSettings settings)
        {
            this.LoadParameter(settings);

            this.TitleCenter = new Vector2(this.ScreenWidth / 2f, 100);
        }
    }
}