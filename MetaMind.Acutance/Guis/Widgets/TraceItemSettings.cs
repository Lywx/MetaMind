namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine.Settings;
    using MetaMind.Engine.Settings.Colors;
    using MetaMind.Runtime.Guis.Widgets;

    using Microsoft.Xna.Framework;

    public class TraceItemSettings : TaskItemSettings
    {
        public Color NameFrameRunningColor = Palette.DarkRed;

        public TraceItemSettings()
        {
            this.RootFrameSize = this.NameFrameSize;

            //-----------------------------------------------------------------
            this.NameFrameRegularColor   = Palette.TransparentColor1;
            this.NameFrameMouseOverColor = Palette.TransparentColor2;
            this.NameFramePendingColor   = Palette.TransparentColor5;

            //-----------------------------------------------------------------
            this.IdSize              = 0.7f;
            this.IdFrameSize         = new Point(24, 24);
            this.IdFrameColor        = Palette.TransparentColor1;
            this.IdFramePendingColor = Palette.TransparentColor5;

            //-----------------------------------------------------------------
            this.ExperienceFrameColor = Palette.TransparentColor1;
        }
    }
}