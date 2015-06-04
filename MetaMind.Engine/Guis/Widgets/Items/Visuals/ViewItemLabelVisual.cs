namespace MetaMind.Engine.Guis.Widgets.Items.Visuals
{
    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Guis.Widgets.Visuals;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class ViewItemLabelVisual : ViewItemComponent, IViewItemVisual
    {
        private readonly Label label;

        public ViewItemLabelVisual(IViewItem item, LabelSettings labelSettings)
            : base(item)
        {
            this.label = new Label(
                () => labelSettings.TextFont,
                labelSettings.Text,
                labelSettings.TextPosition,
                () => labelSettings.TextColor,
                () => labelSettings.TextSize,
                labelSettings.TextHAlign,
                labelSettings.TextVAlign,
                labelSettings.TextLeading,
                labelSettings.TextMonospaced);
        }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            if (!this.Item[ItemState.Item_Is_Active]() && 
                !this.Item[ItemState.Item_Is_Dragging]())
            {
                return;
            }

            this.label.Draw(graphics, time, alpha);
        }
    }
}