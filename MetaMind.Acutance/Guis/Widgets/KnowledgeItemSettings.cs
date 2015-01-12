namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine.Settings.Colors;

    using Microsoft.Xna.Framework;

    public class KnowledgeItemSettings : TraceItemSettings
    {
        public Color NameFrameTitleColor = ColorPalette.DarkRed;

        public KnowledgeItemSettings()
        {
            this.RootFrameSize = this.NameFrameSize;

            this.NameFrameRegularColor = ColorPalette.TransparentColor1;
            this.NameFrameRunningColor = ColorPalette.TransparentColor1;
        }
    }
}