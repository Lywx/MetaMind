namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Guis.Elements.Items;
    using MetaMind.Engine.Settings;

    using Microsoft.Xna.Framework;

    public class TraceItemSettings : ItemSettings
    {
        //---------------------------------------------------------------------
        public Color ErrorColor                    = Color.White;

        //---------------------------------------------------------------------
        public float NameSize                      = 0.7f;

        public Color NameColor                     = Color.White;
        public Font  NameFont                      = Font.InfoSimSunFont;
        public Point NameFrameSize                 = new Point(256, 24);
        public Point NameFrameMargin               = new Point(2, 2);

        //---------------------------------------------------------------------
        public int   NameXLMargin                  = 10;
        public int   NameXRMargin                  = 30;
        public int   NameYTMargin                  = 5;

        //---------------------------------------------------------------------
        public Color NameFrameRegularColor         = ColorPalette.TransparentColor1;
        public Color NameFrameMouseOverColor       = ColorPalette.TransparentColor2;
        public Color NameFramePendingColor         = new Color(200, 200, 0, 2);
        public Color NameFrameModificationColor    = new Color(0, 20, 250, 2);
        public Color NameFrameSelectionColor       = new Color(0, 20, 250, 2);

        //---------------------------------------------------------------------
        public Color IdFramePendingColor           = new Color(200, 200, 0, 2);

        //---------------------------------------------------------------------
        public float ExperienceSize                = 0.7f;
        public Color ExperienceColor               = Color.White;
        public Font  ExperienceFont                = Font.UiStatisticsFont;
        public Point ExperienceFrameSize           = new Point(96, 24);
        public Point ExperienceFrameMargin         = new Point(2, 2);
        public Color ExperienceFrameColor          = ColorPalette.TransparentColor1;

        //---------------------------------------------------------------------
        public Font  HelpFont                      = Font.UiStatisticsFont;
        public float HelpSize                      = 0.75f;
        public Color HelpColor                     = Color.White;

        public TraceItemSettings()
        {
            //-----------------------------------------------------------------
            this.RootFrameSize = this.NameFrameSize;

            //-----------------------------------------------------------------
            this.NameXLMargin = (int)(this.NameXLMargin * this.NameSize);
            this.NameXRMargin = (int)(this.NameXRMargin * this.NameSize);
            this.NameYTMargin = (int)(this.NameYTMargin * this.NameSize);

            //-----------------------------------------------------------------
            this.IdSize       = 0.7f;
            this.IdFrameSize  = new Point(24, 24);
            this.IdFrameColor = ColorPalette.TransparentColor1;
        }
    }
}