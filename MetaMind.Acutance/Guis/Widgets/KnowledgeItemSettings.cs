namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine.Settings;
    using MetaMind.Engine.Settings.Colors;

    using Microsoft.Xna.Framework;

    public class KnowledgeItemSettings : TraceItemSettings
    {
        public Color NameFrameTitleColor = Palette.DarkRed;

        public KnowledgeItemSettings()
        {
            this.RootFrameSize = this.NameFrameSize;

            this.NameFrameRegularColor = Palette.Transparent1;
            this.NameFrameRunningColor = Palette.Transparent1;
        }
    }
}