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

            this.TextFont       = () => settings.Font;
            this.TextColor      = () => settings.Color;
            this.TextSize       = () => settings.Size;
            this.TextHAlignment = settings.HAlignment;
            this.TextVAlignment = settings.VAlignment;
            this.TextLeading    = settings.Leading;
            this.TextMonospaced = settings.Monospaced;
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