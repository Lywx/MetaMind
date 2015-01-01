namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine.Settings;

    using Microsoft.Xna.Framework;

    public class KnowledgeItemSettings : TraceItemSettings
    {
        public Color NameFrameCommandColor = ColorPalette.LightPink;

        public KnowledgeItemSettings()
        {
            this.RootFrameSize = this.NameFrameSize;

            this.NameFrameRegularColor = ColorPalette.TransparentColor1;
            this.NameFrameRunningColor = ColorPalette.TransparentColor1;
        }
    }
}