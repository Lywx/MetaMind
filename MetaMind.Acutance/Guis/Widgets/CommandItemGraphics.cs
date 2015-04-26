namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine;
    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class CommandItemGraphics : TraceItemGraphics
    {
        public CommandItemGraphics(IViewItem item)
            : base(item)
        {
        }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            if (!this.ItemControl.Active && !this.Item.IsEnabled(ItemState.Item_Is_Dragging))
            {
                return;
            }

            this.DrawNameFrame(graphics, 255);

            this.FillNameFrameWith(graphics, this.ItemSettings.NameFrameRunningColor, alpha);

            this.DrawName(255);
            this.DrawIdFrame(255);
            this.DrawId(graphics, 255);
        }

        protected override void DrawName(byte alpha)
        {
            string name = ItemData.Name;

            string text = FontManager.CropMonospacedString(
                ItemControl.Id > 0 ? FormatUtils.Paddle(name, View.Items[ItemControl.Id - 1].ItemData.Name) : name,
                ItemSettings.NameSize,
                ItemSettings.NameFrameSize.X - ItemSettings.NameXLMargin * 2);

            FontManager.DrawMonospacedString(
                ItemSettings.NameFont,
                text,
                this.NameLocation,
                ExtColor.MakeTransparent(ItemSettings.NameColor, alpha),
                ItemSettings.NameSize);
        }
    }
}