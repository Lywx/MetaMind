namespace MetaMind.Engine.Gui.Control.Item.Visuals
{
    using System;
    using Gui.Control.Visuals;
    using Microsoft.Xna.Framework;
    using Service;

    public class ViewItemLabel : Label
    {
        public ViewItemLabel(IViewItem item, LabelSettings settings)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            this.Item = item;

            this.TextFont       = () => settings.TextFont;
            this.TextColor      = () => settings.TextColor;
            this.TextSize       = () => settings.TextSize;
            this.TextHAlignment = settings.TextHAlignment;
            this.TextVAlignment = settings.TextVAlignment;
            this.TextLeading    = settings.TextLeading;
            this.TextMonospaced = settings.TextMonospaced;
        }

        public IViewItem Item { get; }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            if (!this.Item[ItemState.Item_Is_Active]() &&
                !this.Item[ItemState.Item_Is_Dragging]())
            {
                return;
            }

            base.Draw(graphics, time, alpha);
        }
    }
}