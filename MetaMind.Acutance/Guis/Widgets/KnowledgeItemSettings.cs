namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Guis.Elements.Items;
    using MetaMind.Engine.Settings;

    using Microsoft.Xna.Framework;

    public class KnowledgeItemSettings : TraceItemSettings
    {
        public KnowledgeItemSettings()
        {
            //-----------------------------------------------------------------
            this.RootFrameSize = this.NameFrameSize;

            //-----------------------------------------------------------------
            this.NameXLMargin = (int)(this.NameXLMargin * this.NameSize);
            this.NameXRMargin = (int)(this.NameXRMargin * this.NameSize);
            this.NameYTMargin = (int)(this.NameYTMargin * this.NameSize);

            //-----------------------------------------------------------------
            this.IdSize       = 0.7f;
            this.IdFrameSize  = new Point(24, 24);
            this.IdFrameColor = ColorPalette.TransparentColor1;
        }
    }
}