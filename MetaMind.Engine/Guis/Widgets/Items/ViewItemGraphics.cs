namespace MetaMind.Engine.Guis.Widgets.Items
{
    using System;
    using System.Globalization;

    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    using Primtives2D;

    public class ViewItemGraphics : ViewItemComponent, IItemGraphics
    {
        protected Func<Vector2> IdCenterPosition { get; set; }

        protected Func<Vector2> ItemCenterPosition { get; set; }

        public ViewItemGraphics(IViewItem item)
            : base(item)
        {
            var rootFrame = ItemControl.RootFrame;

            IdCenterPosition   = () => new Vector2(rootFrame.Rectangle.Right + 10, rootFrame.Rectangle.Top - 10);
            ItemCenterPosition = () => ExtPoint.ToVector2(rootFrame.Center);
        }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            if (!ItemControl.Active && !Item.IsEnabled(ItemState.Item_Dragging))
            {
                return;
            }

            this.DrawNameFrame(graphics, alpha);
            this.DrawId(graphics, alpha);
        }

        public override void Update(GameTime time)
        {
        }

        protected virtual void DrawId(IGameGraphicsService graphics, byte alpha)
        {
            graphics.String.DrawStringCenteredHV(
                ItemSettings.IdFont,
                ItemControl.Id.ToString(new CultureInfo("en-US")),
                this.IdCenterPosition(),
                ExtColor.MakeTransparent(ItemSettings.IdColor, alpha),
                ItemSettings.IdSize);
        }

        protected virtual void DrawNameFrame(IGameGraphicsService graphics, byte alpha)
        {
            if (Item.IsEnabled(ItemState.Item_Pending))
            {
                this.FillNameFrameWith(graphics, this.ItemSettings.NameFramePendingColor, alpha);
            }
            else if (Item.IsEnabled(ItemState.Item_Mouse_Over) &&
                     Item.IsEnabled(ItemState.Item_Editing))
            {
                this.FillNameFrameWith(graphics, this.ItemSettings.NameFrameModificationColor, alpha);
                this.DrawNameFrameWith(graphics, this.ItemSettings.NameFrameMouseOverColor, alpha);
            }
            else if (!Item.IsEnabled(ItemState.Item_Mouse_Over) &&
                     Item.IsEnabled(ItemState.Item_Editing))
            {
                this.FillNameFrameWith(graphics, this.ItemSettings.NameFrameModificationColor, alpha);
            }
            else if (Item.IsEnabled(ItemState.Item_Mouse_Over) &&
                     Item.IsEnabled(ItemState.Item_Selected))
            {
                this.FillNameFrameWith(graphics, this.ItemSettings.NameFrameSelectionColor, alpha);
                this.DrawNameFrameWith(graphics, this.ItemSettings.NameFrameMouseOverColor, alpha);
            }
            else if (Item.IsEnabled(ItemState.Item_Mouse_Over) &&
                     !Item.IsEnabled(ItemState.Item_Selected))
            {
                this.FillNameFrameWith(graphics, this.ItemSettings.NameFrameRegularColor, alpha);
                this.DrawNameFrameWith(graphics, this.ItemSettings.NameFrameMouseOverColor, alpha);
            }
            else if (Item.IsEnabled(ItemState.Item_Selected))
            {
                this.FillNameFrameWith(graphics, this.ItemSettings.NameFrameSelectionColor, alpha);
            }
            else
            {
                this.FillNameFrameWith(graphics, this.ItemSettings.NameFrameRegularColor, alpha);
            }
        }

        protected void DrawNameFrameWith(IGameGraphicsService graphics, Color color, byte alpha)
        {
            Primitives2D.DrawRectangle(
                graphics.SpriteBatch,
                ExtRectangle.Crop(ItemControl.NameFrame.Rectangle, ItemSettings.NameFrameMargin),
                color.MakeTransparent(alpha),
                1f);
        }

        protected void FillNameFrameWith(IGameGraphicsService graphics, Color color, byte alpha)
        {
            Primitives2D.FillRectangle(
                graphics.SpriteBatch,
                ExtRectangle.Crop(ItemControl.NameFrame.Rectangle, ItemSettings.NameFrameMargin),
                color.MakeTransparent(alpha));
        }
    }
}