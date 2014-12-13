namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine.Settings;
    using MetaMind.Perseverance.Guis.Widgets;

    using Microsoft.Xna.Framework;

    public class EventItemSettings : TraceItemSettings
    {
        public Color NameFrameTranitionColor = ColorPalette.LightPink;

        public EventItemSettings()
        {
            this.RootFrameSize = this.NameFrameSize;

            this.NameFrameRegularColor   = ColorPalette.LightGreen;
            this.NameFrameMouseOverColor = ColorPalette.TransparentColor2;
        }
    }
}