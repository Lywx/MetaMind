namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine.Concepts;
    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Guis.Widgets.Items;

    using Microsoft.Xna.Framework;

    using Primtives2D;

    public class ModuleItemGraphics : ViewItemBasicGraphics
    {
        private const string HelpInformation = "N:ame";

        #region Constructors

        public ModuleItemGraphics(IViewItem item)
            : base(item)
        {
        }

        #endregion Constructors

        #region Structure Position

        protected Vector2 ExperienceLocation
        {
            get { return PointExt.ToVector2(this.ItemControl.ExperienceFrame.Center); }
        }

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
                    this.ItemControl.NameFrame.Location.Y + this.ItemSettings.NameYTMargin);
            }
        }


        #endregion Structure Position

        #region Update and Draw

        public override void Draw(GameTime gameTime, byte alpha)
        {
            if (!this.ItemControl.Active && !this.Item.IsEnabled(ItemState.Item_Dragging))
            {
                return;
            }

            this.DrawNameFrame(255);

            if (ItemData.IsPopulating)
            {
                this.FillNameFrameWith(this.ItemSettings.NameFrameRunningColor, alpha);
            }

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
                this.ItemSettings.ExperienceFont,
                string.Format("{0:hh\\:mm\\:ss}", ((Experience)this.ItemData.Experience).Duration),
                this.ExperienceLocation,
                ColorExt.MakeTransparent(this.ItemSettings.ExperienceColor, alpha),
                this.ItemSettings.ExperienceSize);
        }

        protected void DrawExperienceFrame(byte alpha)
        {
            Primitives2D.FillRectangle(
                ScreenManager.SpriteBatch,
                RectangleExt.Crop(this.ItemControl.ExperienceFrame.Rectangle, this.ItemSettings.ExperienceFrameMargin),
                ColorExt.MakeTransparent(this.ItemSettings.ExperienceFrameColor, alpha));

        }

        protected void DrawIdFrame(byte alpha)
        {
            Primitives2D.FillRectangle(
                ScreenManager.SpriteBatch,
                RectangleExt.Crop(this.ItemControl.IdFrame.Rectangle, this.ItemSettings.IdFrameMargin),
                this.Item.IsEnabled(ItemState.Item_Pending)
                    ? ColorExt.MakeTransparent(this.ItemSettings.IdFramePendingColor, alpha)
                    : ColorExt.MakeTransparent(this.ItemSettings.IdFrameColor, alpha));
        }

        protected void DrawName(byte alpha)
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
                string text = FontManager.CropText(
                    this.ItemSettings.NameFont,
                    this.ItemData.Name,
                    this.ItemSettings.NameSize,
                    this.ItemSettings.NameFrameSize.X - this.ItemSettings.NameXLMargin * 2);
                
                FontManager.DrawText(
                    this.ItemSettings.NameFont,
                    text,
                    this.NameLocation,
                    ColorExt.MakeTransparent(this.ItemSettings.NameColor, alpha),
                    this.ItemSettings.NameSize);
            }
        }

        #endregion Draw
    }
}