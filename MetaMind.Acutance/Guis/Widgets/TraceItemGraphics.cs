namespace MetaMind.Acutance.Guis.Widgets
{
    using System;
    using System.Globalization;

    using C3.Primtive2DXna;

    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Guis.Elements.Items;
    using MetaMind.Engine.Guis.Elements.ViewItems;
    using MetaMind.Perseverance;

    using Microsoft.Xna.Framework;

    public class TraceItemGraphics : ViewItemBasicGraphics
    {
        private const string HelpInformation = "N-ame ";

        #region Constructors

        public TraceItemGraphics(IViewItem item)
            : base(item)
        {
        }

        #endregion Constructors

        #region Structure Position

        protected override Vector2 IdCenter
        {
            get { return PointExt.ToVector2(this.ItemControl.IdFrame.Center); }
        }

        private Vector2 HelpLocation
        {
            get { return this.NameLocation; }
        }

        private Vector2 NameLocation
        {
            get
            {
                return new Vector2(
                    this.ItemControl.NameFrame.Location.X + this.ItemSettings.NameXLMargin,
                    this.ItemControl.NameFrame.Location.Y + this.ItemSettings.NameYTMargin
                    );
            }
        }

        private Rectangle RandomHighlight(GameTime gameTime, int flashLength, Rectangle rectangle)
        {
            var thick = Perseverance.Adventure.Random.Next(flashLength);
            return new Rectangle(
                rectangle.X - thick,
                rectangle.Y - thick,
                rectangle.Width + thick * 2,
                rectangle.Height + thick * 2);
        }

        private Rectangle SinwaveHighlight(GameTime gameTime, int flashLength, Rectangle rectangle)
        {
            var thick = (int)(flashLength * Math.Abs(Math.Sin(gameTime.TotalGameTime.TotalSeconds * 2)));
            return new Rectangle(
                rectangle.X - thick,
                rectangle.Y - thick,
                rectangle.Width + thick * 2,
                rectangle.Height + thick * 2);
        }

        #endregion Structure Position

        #region Draw

        public override void Draw(GameTime gameTime, byte alpha)
        {
            if (!this.ItemControl.Active && !this.Item.IsEnabled(ItemState.Item_Dragging))
            {
                return;
            }

            this.DrawNameFrame(alpha);
            this.DrawName(alpha);
            this.DrawIdFrame(alpha);
            this.DrawId(alpha);
        }

        private void DrawIdFrame(byte alpha)
        {
            Primitives2D.FillRectangle(
                ScreenManager.SpriteBatch,
                RectangleExt.Crop(this.ItemControl.IdFrame.Rectangle, this.ItemSettings.IdFrameMargin),
                this.Item.IsEnabled(ItemState.Item_Pending)
                    ? ColorExt.MakeTransparent(this.ItemSettings.IdFramePendingColor, alpha)
                    : ColorExt.MakeTransparent(this.ItemSettings.IdFrameColor, alpha));
        }

        private void DrawName(byte alpha)
        {
            if (this.Item.IsEnabled(ItemState.Item_Pending))
            {
                FontManager.DrawText(
                    this.ItemSettings.HelpFont,
                    HelpInformation,
                    this.HelpLocation,
                    ColorExt.MakeTransparent(this.ItemSettings.HelpColor, alpha),
                    this.ItemSettings.HelpSize);
            }
            else
            {
                FontManager.DrawText(
                    this.ItemSettings.NameFont,
                    this.ItemData.Name,
                    this.NameLocation,
                    ColorExt.MakeTransparent(this.ItemSettings.NameColor, alpha),
                    this.ItemSettings.NameSize);
            }
        }

        private void DrawNameFrame(byte alpha)
        {
            if (this.Item.IsEnabled(ItemState.Item_Mouse_Over) &&
                this.Item.IsEnabled(ItemState.Item_Editing))
            {
                this.FillNameFrameWith(this.ItemSettings.NameFrameModificationColor);
                this.DrawNameFrameWith(this.ItemSettings.NameFrameMouseOverColor);
            }
            else if (!this.Item.IsEnabled(ItemState.Item_Mouse_Over) &&
                     this.Item.IsEnabled(ItemState.Item_Editing))
            {
                this.FillNameFrameWith(this.ItemSettings.NameFrameModificationColor);
            }
            else if (this.Item.IsEnabled(ItemState.Item_Mouse_Over) &&
                     this.Item.IsEnabled(ItemState.Item_Selected))
            {
                this.FillNameFrameWith(this.ItemSettings.NameFrameSelectionColor);
                this.DrawNameFrameWith(this.ItemSettings.NameFrameMouseOverColor);
            }
            else if (this.Item.IsEnabled(ItemState.Item_Mouse_Over) &&
                     !this.Item.IsEnabled(ItemState.Item_Selected))
            {
                this.DrawNameFrameWith(this.ItemSettings.NameFrameMouseOverColor);
            }
            else if (this.Item.IsEnabled(ItemState.Item_Selected))
            {
                this.FillNameFrameWith(this.ItemSettings.NameFrameSelectionColor);
            }
            else
            {
                this.FillNameFrameWith(this.ItemSettings.NameFrameRegularColor);
            }
        }

        private void DrawNameFrameWith(Color color)
        {
            Primitives2D.DrawRectangle(
                ScreenManager.SpriteBatch,
                RectangleExt.Crop(this.ItemControl.NameFrame.Rectangle, this.ItemSettings.NameFrameMargin),
                color,
                1f);
        }
        private void FillNameFrameWith(Color color)
        {
            Primitives2D.FillRectangle(
                ScreenManager.SpriteBatch,
                RectangleExt.Crop(this.ItemControl.NameFrame.Rectangle, this.ItemSettings.NameFrameMargin),
                color);
        }

        #endregion Draw
    }
}