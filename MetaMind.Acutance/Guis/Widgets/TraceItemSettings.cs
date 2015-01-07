namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine.Settings.Colors;
    using MetaMind.Perseverance.Guis.Widgets;

    using Microsoft.Xna.Framework;

    public class TraceItemSettings : TaskItemSettings
    {
        public Color NameFrameRunningColor = ColorPalette.LightRed;

        public TraceItemSettings()
        {
            this.RootFrameSize = this.NameFrameSize;

            //-----------------------------------------------------------------
            this.NameFrameRegularColor   = ColorPalette.TransparentColor1;
            this.NameFrameMouseOverColor = ColorPalette.TransparentColor2;

            //-----------------------------------------------------------------
            this.IdSize       = 0.7f;
            this.IdFrameSize  = new Point(24, 24);
            this.IdFrameColor = ColorPalette.TransparentColor1;

            //-----------------------------------------------------------------
            this.ExperienceFrameColor = ColorPalette.TransparentColor1;
        }
    }
}