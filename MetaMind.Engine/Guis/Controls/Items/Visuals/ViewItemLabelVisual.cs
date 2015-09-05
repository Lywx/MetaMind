namespace MetaMind.Engine.Guis.Widgets.Items.Visuals
{
    using Microsoft.Xna.Framework;
    using Services;
    using Widgets.Visuals;

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

        public Label Label => this.label;

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            if (!this.Item[ItemState.Item_Is_Active]() && 
                !this.Item[ItemState.Item_Is_Dragging]())
            {
                return;
            }

            this.Label.Draw(graphics, time, alpha);
        }
    }
}