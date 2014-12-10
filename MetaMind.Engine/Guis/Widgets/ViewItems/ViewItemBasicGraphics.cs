namespace MetaMind.Engine.Guis.Widgets.ViewItems
{
    using System.Globalization;

    using C3.Primtive2DXna;

    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Guis.Widgets.Items;

    using Microsoft.Xna.Framework;

    public class ViewItemBasicGraphics : ViewItemComponent, IItemGraphics
    {
        public ViewItemBasicGraphics(IViewItem item)
            : base(item)
        {
        }

        protected Point Center
        {
            get { return this.ItemControl.RootFrame.Center; }
        }

        protected virtual Vector2 IdCenter
        {
            get
            {
                return new Vector2(
                    this.ItemControl.RootFrame.Rectangle.Right + 10,
                    this.ItemControl.RootFrame.Rectangle.Top - 10);
            }
        }

        public virtual void Draw(GameTime gameTime, byte alpha)
        {
            if (!this.ItemControl.Active && !this.Item.IsEnabled(ItemState.Item_Dragging))
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
                this.ItemSettings.IdFont,
                this.ItemControl.Id.ToString(new CultureInfo("en-US")),
                this.IdCenter,
                ColorExt.MakeTransparent(this.ItemSettings.IdColor, alpha),
                this.ItemSettings.IdSize);
        }

        protected virtual void DrawNameFrame(byte alpha)
        {
            if (this.Item.IsEnabled(ItemState.Item_Pending))
            {
                this.FillNameFrameWith(this.ItemSettings.NameFramePendingColor, alpha);
            }
            else if (this.Item.IsEnabled(ItemState.Item_Mouse_Over) &&
                     this.Item.IsEnabled(ItemState.Item_Editing))
            {
                this.FillNameFrameWith(this.ItemSettings.NameFrameModificationColor, alpha);
                this.DrawNameFrameWith(this.ItemSettings.NameFrameMouseOverColor, alpha);
            }
            else if (!this.Item.IsEnabled(ItemState.Item_Mouse_Over) &&
                     this.Item.IsEnabled(ItemState.Item_Editing))
            {
                this.FillNameFrameWith(this.ItemSettings.NameFrameModificationColor, alpha);
            }
            else if (this.Item.IsEnabled(ItemState.Item_Mouse_Over) &&
                     this.Item.IsEnabled(ItemState.Item_Selected))
            {
                this.FillNameFrameWith(this.ItemSettings.NameFrameSelectionColor, alpha);
                this.DrawNameFrameWith(this.ItemSettings.NameFrameMouseOverColor, alpha);
            }
            else if (this.Item.IsEnabled(ItemState.Item_Mouse_Over) &&
                     !this.Item.IsEnabled(ItemState.Item_Selected))
            {
                this.FillNameFrameWith(this.ItemSettings.NameFrameRegularColor, alpha);
                this.DrawNameFrameWith(this.ItemSettings.NameFrameMouseOverColor, alpha);
            }
            else if (this.Item.IsEnabled(ItemState.Item_Selected))
            {
                this.FillNameFrameWith(this.ItemSettings.NameFrameSelectionColor, alpha);
            }
            else
            {
                this.FillNameFrameWith(this.ItemSettings.NameFrameRegularColor, alpha);
            }
        }

        protected void DrawNameFrameWith(Color color, byte alpha)
        {
            Primitives2D.DrawRectangle(
                ScreenManager.SpriteBatch,
                RectangleExt.Crop(this.ItemControl.NameFrame.Rectangle, this.ItemSettings.NameFrameMargin),
                color.MakeTransparent(alpha),
                1f);
        }

        protected void FillNameFrameWith(Color color, byte alpha)
        {
            Primitives2D.FillRectangle(
                ScreenManager.SpriteBatch,
                RectangleExt.Crop(this.ItemControl.NameFrame.Rectangle, this.ItemSettings.NameFrameMargin),
                color.MakeTransparent(alpha));
        }
    }
}