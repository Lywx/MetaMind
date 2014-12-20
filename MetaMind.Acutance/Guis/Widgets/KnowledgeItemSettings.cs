namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine.Settings;

    public class KnowledgeItemSettings : TraceItemSettings
    {
        public KnowledgeItemSettings()
        {
            this.RootFrameSize = this.NameFrameSize;

            this.NameFrameRegularColor = ColorPalette.TransparentColor1;
            this.NameFrameRunningColor = ColorPalette.TransparentColor1;
        }
    }
}