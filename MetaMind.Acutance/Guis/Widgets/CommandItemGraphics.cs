namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine;
    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Concepts;
    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Guis.Widgets.Items;
    using Microsoft.Xna.Framework;

    public class CommandItemGraphics : TraceItemGraphics
    {
        public CommandItemGraphics(IViewItem item)
            : base(item)
        {
        }

        public override void Draw(IGameGraphics gameGraphics, GameTime gameTime, byte alpha)
        {
            if (!ItemControl.Active && !Item.IsEnabled(ItemState.Item_Dragging))
            {
                return;
            }

            this.DrawNameFrame(gameGraphics, 255);

            this.FillNameFrameWith(gameGraphics, this.ItemSettings.NameFrameRunningColor, alpha);

            this.DrawName(255);
            this.DrawIdFrame(255);
            this.DrawId(TODO, 255);
        }

        protected override void DrawName(byte alpha)
        {
            string name = ItemData.Name;

            string text = FontManager.CropMonospacedString(
                ItemControl.Id > 0 ? FormatHelper.Paddle(name, View.Items[ItemControl.Id - 1].ItemData.Name) : name,
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