namespace MetaMind.Perseverance.Guis.Widgets
{
    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Settings;
    using MetaMind.Engine.Settings.Colors;

    using Microsoft.Xna.Framework;

    public class TaskItemSettings : ItemSettings, IAdjustable
    {
        //---------------------------------------------------------------------
        public float NameSize                      = 0.7f;

        public Color NameColor                     = Color.White;

        public Font  NameFont                      = Font.UiContentFont;

        public Point NameFrameSize                 = new Point(256, 24);

        public Point NameFrameMargin               = new Point(2, 2);

        //---------------------------------------------------------------------
        public int   NameXLMargin                  = 10;

        public int   NameXRMargin                  = 30;

        public int   NameYTMargin                  = 5;

        //---------------------------------------------------------------------
        public Color NameFrameRegularColor         = new Color(0, 0, 0, 0);

        public Color NameFrameMouseOverColor       = new Color(23, 41, 61, 2);

        public Color NameFramePendingColor         = new Color(200, 200, 0, 2);

        public Color NameFrameModificationColor    = new Color(0, 0, 0, 0);

        public Color NameFrameSelectionColor       = Palette.LightBlue;

        public Color NameFrameSynchronizationColor = Palette.LightBlue;

        //---------------------------------------------------------------------
        public Color IdFramePendingColor           = Palette.LightYellow;

        //---------------------------------------------------------------------
        public float ExperienceSize                = 0.7f;

        public Color ExperienceColor               = Color.White;

        public Font  ExperienceFont                = Font.UiStatisticsFont;

        public Point ExperienceFrameSize           = new Point(96, 24);

        public Point ExperienceFrameMargin         = new Point(2, 2);

        public Color ExperienceFrameColor          = Palette.TransparentColor1;

        //---------------------------------------------------------------------
        public float ProgressSize                  = 0.7f;

        public Color ProgressColor                 = Color.White;

        public Font  ProgressFont                  = Font.UiStatisticsFont;

        public Point ProgressFrameSize             = new Point(96, 24);

        public Color ProgressFrameColor            = Palette.TransparentColor1;

        public Point ProgressFrameMargin           = new Point(2, 2);

        public Color ProgressBarColor              = Palette.TransparentColor2;

        //---------------------------------------------------------------------
        public Font  HelpFont                      = Font.UiStatisticsFont;

        public float HelpSize                      = 0.75f;
        
        public Color HelpColor                     = Color.White;

        //---------------------------------------------------------------------
        public TaskItemSettings()
        {
            this.RootFrameSize = this.NameFrameSize;

            //-----------------------------------------------------------------
            this.NameXLMargin = (int)(this.NameXLMargin * this.NameSize);
            this.NameXRMargin = (int)(this.NameXRMargin * this.NameSize);
            this.NameYTMargin = (int)(this.NameYTMargin * this.NameSize);
           
            //-----------------------------------------------------------------
            this.IdSize       = 0.7f;
            this.IdFrameSize  = new Point(24, 24);
        }

        public virtual void Configure()
        {
            this.RootFrameSize = this.NameFrameSize;
        }
    }

    public interface IAdjustable
    {
        void Configure();
    }
}