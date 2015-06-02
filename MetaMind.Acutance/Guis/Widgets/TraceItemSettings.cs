namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine.Settings;
    using MetaMind.Engine.Settings.Colors;
    using Microsoft.Xna.Framework;

    public class TraceItemSettings : TaskItemSettings
    {
        public Color NameFrameRunningColor = Palette.DarkRed;

        public TraceItemSettings()
        {
            this.RootFrameSize = this.NameFrameSize;

            //-----------------------------------------------------------------
            this.NameFrameRegularColor   = Palette.Transparent1;
            this.NameFrameMouseOverColor = Palette.Transparent2;
            this.NameFramePendingColor   = Palette.Transparent5;

            //-----------------------------------------------------------------
            this.IdSize              = 0.7f;
            this.IdFrameSize         = new Point(24, 24);
            this.IdFrameColor        = Palette.Transparent1;
            this.IdFramePendingColor = Palette.Transparent5;

            //-----------------------------------------------------------------
            this.ExperienceFrameColor = Palette.Transparent1;
        }
    }
}