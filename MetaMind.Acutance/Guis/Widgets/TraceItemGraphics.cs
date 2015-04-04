namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine.Concepts;
    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Guis.Widgets.Items;

    using Microsoft.Xna.Framework;

    using Primtives2D;

    public class TraceItemGraphics : ViewItemBasicGraphics
    {
        #region Constructors

        public TraceItemGraphics(IViewItem item)
            : base(item)
        {
        }

        #endregion Constructors

        #region Structure Position

        protected virtual Vector2 ExperienceLocation
        {
            get { return PointExt.ToVector2(ItemControl.ExperienceFrame.Center); }
        }

        protected virtual Vector2 HelpLocation
        {
            get { return this.NameLocation; }
        }

        protected override Vector2 IdCenter
        {
            get { return PointExt.ToVector2(ItemControl.IdFrame.Center); }
        }

        protected virtual Vector2 NameLocation
        {
            get
            {
                return new Vector2(
                    ItemControl.NameFrame.Location.X + ItemSettings.NameXLMargin,
                    ItemControl.NameFrame.Location.Y + ItemSettings.NameYTMargin);
            }
        }

        #endregion Structure Position

        #region Extras

        protected virtual string HelpInformation
        {
            get
            {
                return "N:ame";
            }
        }

        #endregion

        #region Update and Draw

        public override void Draw(GameTime gameTime, byte alpha)
        {
            if (!ItemControl.Active && !Item.IsEnabled(ItemState.Item_Dragging))
            {
                return;
            }

            this.DrawNameFrame(255);

            this.FillNameFrameWith(ItemSettings.NameFrameRunningColor, alpha);

            this.DrawName(255);
            this.DrawExperienceFrame(255);
            this.DrawExperience(255);
            this.DrawIdFrame(255);
            this.DrawId(255);
        }

        public override void Update(GameTime gameTime)
        {
        }

        protected virtual void DrawExperience(byte alpha)
        {
            FontManager.DrawCenteredText(
                ItemSettings.ExperienceFont,
                string.Format("{0:hh\\:mm\\:ss}", ((Experience)ItemData.Experience).Duration),
                this.ExperienceLocation,
                ColorExt.MakeTransparent(ItemSettings.ExperienceColor, alpha),
                ItemSettings.ExperienceSize);
        }

        protected void DrawExperienceFrame(byte alpha)
        {
            Primitives2D.FillRectangle(
                ScreenManager.SpriteBatch,
                RectangleExt.Crop(ItemControl.ExperienceFrame.Rectangle, ItemSettings.ExperienceFrameMargin),
                ColorExt.MakeTransparent(ItemSettings.ExperienceFrameColor, alpha));
        }

        protected void DrawIdFrame(byte alpha)
        {
            Primitives2D.FillRectangle(
                ScreenManager.SpriteBatch,
                RectangleExt.Crop(ItemControl.IdFrame.Rectangle, ItemSettings.IdFrameMargin),
                Item.IsEnabled(ItemState.Item_Pending)
                    ? ColorExt.MakeTransparent(ItemSettings.IdFramePendingColor, alpha)
                    : ColorExt.MakeTransparent(ItemSettings.IdFrameColor, alpha));
        }

        protected virtual void DrawName(byte alpha)
        {
            if (Item.IsEnabled(ItemState.Item_Pending))
            {
                FontManager.DrawText(
                    ItemSettings.HelpFont,
                    HelpInformation,
                    this.HelpLocation,
                    ColorExt.MakeTransparent(ItemSettings.HelpColor, alpha),
                    ItemSettings.HelpSize);
            }
            else
            {
                string text = FontManager.CropMonoSpacedString(
                    ItemData.Name,
                    ItemSettings.NameSize,
                    ItemSettings.NameFrameSize.X - ItemSettings.NameXLMargin * 2);
                
                FontManager.DrawMonoSpacedText(
                    ItemSettings.NameFont,
                    text,
                    this.NameLocation,
                    ColorExt.MakeTransparent(ItemSettings.NameColor, alpha),
                    ItemSettings.NameSize);
            }
        }

        #endregion Draw
    }
}