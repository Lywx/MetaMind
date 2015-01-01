namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine.Settings;

    using Microsoft.Xna.Framework;

    public class CommandItemSettings : TraceItemSettings
    {
        public Color NameFrameTransitionColor = ColorPalette.LightPink;

        public CommandItemSettings()
        {
            this.RootFrameSize = this.NameFrameSize;

            this.NameFrameRegularColor   = ColorPalette.TransparentColor1;
            this.NameFrameMouseOverColor = ColorPalette.TransparentColor2;

            this.NameFrameRunningColor   = ColorPalette.LightGrey;
        }
    }
}