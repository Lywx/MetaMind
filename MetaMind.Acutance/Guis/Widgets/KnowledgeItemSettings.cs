namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine.Settings;

    using Microsoft.Xna.Framework;

    public class KnowledgeItemSettings : TraceItemSettings
    {
        public KnowledgeItemSettings()
        {
            this.NameFrameSize         = new Point(GraphicsSettings.Width - this.IdFrameSize.X, 24);
            this.NameFrameStoppedColor = Color.Transparent;

            this.RootFrameSize = this.NameFrameSize;
        }
    }
}