namespace MetaMind.Engine.Guis.Widgets.Items
{
    using System.Globalization;

    using MetaMind.Engine.Extensions;

    using Microsoft.Xna.Framework;

    using Primtives2D;

    public class ViewItemBasicGraphics : ViewItemComponent, IItemGraphics
    {
        public ViewItemBasicGraphics(IViewItem item)
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

        public virtual void Draw(GameTime gameTime, byte alpha)
        {
            if (!ItemControl.Active && !Item.IsEnabled(ItemState.Item_Dragging))
            {
                return;
            }

            this.DrawNameFrame(alpha);
            this.DrawId(alpha);
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        protected virtual void DrawId(byte alpha)
        {
            FontManager.DrawCenteredText(
                ItemSettings.IdFont,
                ItemControl.Id.ToString(new CultureInfo("en-US")),
                this.IdCenter,
                ColorExt.MakeTransparent(ItemSettings.IdColor, alpha),
                ItemSettings.IdSize);
        }

        protected virtual void DrawNameFrame(byte alpha)
        {
            if (Item.IsEnabled(ItemState.Item_Pending))
            {
                this.FillNameFrameWith(ItemSettings.NameFramePendingColor, alpha);
            }
            else if (Item.IsEnabled(ItemState.Item_Mouse_Over) &&
                     Item.IsEnabled(ItemState.Item_Editing))
            {
                this.FillNameFrameWith(ItemSettings.NameFrameModificationColor, alpha);
                this.DrawNameFrameWith(ItemSettings.NameFrameMouseOverColor, alpha);
            }
            else if (!Item.IsEnabled(ItemState.Item_Mouse_Over) &&
                     Item.IsEnabled(ItemState.Item_Editing))
            {
                this.FillNameFrameWith(ItemSettings.NameFrameModificationColor, alpha);
            }
            else if (Item.IsEnabled(ItemState.Item_Mouse_Over) &&
                     Item.IsEnabled(ItemState.Item_Selected))
            {
                this.FillNameFrameWith(ItemSettings.NameFrameSelectionColor, alpha);
                this.DrawNameFrameWith(ItemSettings.NameFrameMouseOverColor, alpha);
            }
            else if (Item.IsEnabled(ItemState.Item_Mouse_Over) &&
                     !Item.IsEnabled(ItemState.Item_Selected))
            {
                this.FillNameFrameWith(ItemSettings.NameFrameRegularColor, alpha);
                this.DrawNameFrameWith(ItemSettings.NameFrameMouseOverColor, alpha);
            }
            else if (Item.IsEnabled(ItemState.Item_Selected))
            {
                this.FillNameFrameWith(ItemSettings.NameFrameSelectionColor, alpha);
            }
            else
            {
                this.FillNameFrameWith(ItemSettings.NameFrameRegularColor, alpha);
            }
        }

        protected void DrawNameFrameWith(Color color, byte alpha)
        {
            Primitives2D.DrawRectangle(
                ScreenManager.SpriteBatch,
                RectangleExt.Crop(ItemControl.NameFrame.Rectangle, ItemSettings.NameFrameMargin),
                color.MakeTransparent(alpha),
                1f);
        }

        protected void FillNameFrameWith(Color color, byte alpha)
        {
            Primitives2D.FillRectangle(
                ScreenManager.SpriteBatch,
                RectangleExt.Crop(ItemControl.NameFrame.Rectangle, ItemSettings.NameFrameMargin),
                color.MakeTransparent(alpha));
        }
    }
}