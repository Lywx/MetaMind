namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine.Settings;

    using Microsoft.Xna.Framework;

    public class CallItemSettings : TraceItemSettings
    {
        public Color NameFrameTransitionColor = ColorPalette.LightPink;

        public CallItemSettings()
        {
            this.RootFrameSize = this.NameFrameSize;

            this.NameFrameRegularColor   = ColorPalette.TransparentColor1;
            this.NameFrameMouseOverColor = ColorPalette.TransparentColor2;

            this.NameFrameRunningColor   = ColorPalette.LightGrey;
        }
    }
}