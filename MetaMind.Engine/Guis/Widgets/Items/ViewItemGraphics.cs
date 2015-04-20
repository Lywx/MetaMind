namespace MetaMind.Engine.Guis.Widgets.Items
{
    using System.Globalization;

    using Microsoft.Xna.Framework;

    using Primtives2D;

    public class ViewItemGraphics : ViewItemComponent, IItemGraphics
    {
        public ViewItemGraphics(IViewItem item)
            : base(item)
        {
        }

        protected Point Center
        {
            get { return ItemControl.RootFrame.Center; }
        }

        protected virtual Vector2 IdCenter
        {
            get
            {
                return new Vector2(
                    ItemControl.RootFrame.Rectangle.Right + 10,
                    ItemControl.RootFrame.Rectangle.Top - 10);
            }
        }

        public override void Draw(IGameGraphics gameGraphics, GameTime gameTime, byte alpha)
        {
            if (!ItemControl.Active && !Item.IsEnabled(ItemState.Item_Dragging))
            {
                return;
            }

            this.DrawNameFrame(gameGraphics, alpha);
            this.DrawId(gameGraphics, alpha);
        }

        public override void Update(GameTime gameTime)
        {
        }

        protected virtual void DrawId(IGameGraphics gameGraphics, byte alpha)
        {
            gameGraphics.StringDrawer.DrawStringCenteredHV(
                ItemSettings.IdFont,
                ItemControl.Id.ToString(new CultureInfo("en-US")),
                this.IdCenter,
                ExtColor.MakeTransparent(ItemSettings.IdColor, alpha),
                ItemSettings.IdSize);
        }

        protected virtual void DrawNameFrame(IGameGraphics gameGraphics, byte alpha)
        {
            if (Item.IsEnabled(ItemState.Item_Pending))
            {
                this.FillNameFrameWith(gameGraphics, this.ItemSettings.NameFramePendingColor, alpha);
            }
            else if (Item.IsEnabled(ItemState.Item_Mouse_Over) &&
                     Item.IsEnabled(ItemState.Item_Editing))
            {
                this.FillNameFrameWith(gameGraphics, this.ItemSettings.NameFrameModificationColor, alpha);
                this.DrawNameFrameWith(gameGraphics, this.ItemSettings.NameFrameMouseOverColor, alpha);
            }
            else if (!Item.IsEnabled(ItemState.Item_Mouse_Over) &&
                     Item.IsEnabled(ItemState.Item_Editing))
            {
                this.FillNameFrameWith(gameGraphics, this.ItemSettings.NameFrameModificationColor, alpha);
            }
            else if (Item.IsEnabled(ItemState.Item_Mouse_Over) &&
                     Item.IsEnabled(ItemState.Item_Selected))
            {
                this.FillNameFrameWith(gameGraphics, this.ItemSettings.NameFrameSelectionColor, alpha);
                this.DrawNameFrameWith(gameGraphics, this.ItemSettings.NameFrameMouseOverColor, alpha);
            }
            else if (Item.IsEnabled(ItemState.Item_Mouse_Over) &&
                     !Item.IsEnabled(ItemState.Item_Selected))
            {
                this.FillNameFrameWith(gameGraphics, this.ItemSettings.NameFrameRegularColor, alpha);
                this.DrawNameFrameWith(gameGraphics, this.ItemSettings.NameFrameMouseOverColor, alpha);
            }
            else if (Item.IsEnabled(ItemState.Item_Selected))
            {
                this.FillNameFrameWith(gameGraphics, this.ItemSettings.NameFrameSelectionColor, alpha);
            }
            else
            {
                this.FillNameFrameWith(gameGraphics, this.ItemSettings.NameFrameRegularColor, alpha);
            }
        }

        protected void DrawNameFrameWith(IGameGraphics gameGraphics, Color color, byte alpha)
        {
            Primitives2D.DrawRectangle(
                gameGraphics.Screen.SpriteBatch,
                ExtRectangle.Crop(ItemControl.NameFrame.Rectangle, ItemSettings.NameFrameMargin),
                color.MakeTransparent(alpha),
                1f);
        }

        protected void FillNameFrameWith(IGameGraphics gameGraphics, Color color, byte alpha)
        {
            Primitives2D.FillRectangle(
                gameGraphics.Screen.SpriteBatch,
                ExtRectangle.Crop(ItemControl.NameFrame.Rectangle, ItemSettings.NameFrameMargin),
                color.MakeTransparent(alpha));
        }
    }
}