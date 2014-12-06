namespace MetaMind.Acutance.Guis.Widgets
{
    using C3.Primtive2DXna;

    using MetaMind.Engine.Concepts;
    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Guis.Elements.Items;
    using MetaMind.Engine.Guis.Elements.ViewItems;

    using Microsoft.Xna.Framework;

    public class TraceItemGraphics : ViewItemBasicGraphics
    {
        private const string HelpInformation = "N:ame";

        #region Constructors

        public TraceItemGraphics(IViewItem item)
            : base(item)
        {
        }

        #endregion Constructors

        #region Structure Position

        protected override Vector2 IdCenter
        {
            get { return PointExt.ToVector2(ItemControl.IdFrame.Center); }
        }

        private Vector2 HelpLocation
        {
            get { return this.NameLocation; }
        }

        private Vector2 ExperienceLocation
        {
            get { return PointExt.ToVector2(ItemControl.ExperienceFrame.Center); }
        }


        private Vector2 NameLocation
        {
            get
            {
                return new Vector2(
                    ItemControl.NameFrame.Location.X + ItemSettings.NameXLMargin,
                    ItemControl.NameFrame.Location.Y + ItemSettings.NameYTMargin);
            }
        }


        #endregion Structure Position

        #region Update and Draw

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(GameTime gameTime, byte alpha)
        {
            if (!ItemControl.Active && !Item.IsEnabled(ItemState.Item_Dragging))
            {
                return;
            }

            this.DrawNameFrame(alpha);
            this.DrawName(alpha);
            this.DrawExperienceFrame(alpha);
            this.DrawExperience(alpha);
            this.DrawIdFrame(alpha);
            this.DrawId(alpha);
        }

        private void DrawExperienceFrame(byte alpha)
        {
            Primitives2D.FillRectangle(
                ScreenManager.SpriteBatch,
                RectangleExt.Crop(ItemControl.ExperienceFrame.Rectangle, ItemSettings.ExperienceFrameMargin),
                ColorExt.MakeTransparent(ItemSettings.ExperienceFrameColor, alpha));

        }

        private void DrawExperience(byte alpha)
        {
                FontManager.DrawCenteredText(
                    ItemSettings.ExperienceFont,
                    string.Format("{0:hh\\:mm\\:ss}", ((Experience)ItemData.Experience).Duration),
                    this.ExperienceLocation,
                    ColorExt.MakeTransparent(ItemSettings.ExperienceColor, alpha),
                    ItemSettings.ExperienceSize);
        }

        private void DrawIdFrame(byte alpha)
        {
            Primitives2D.FillRectangle(
                ScreenManager.SpriteBatch,
                RectangleExt.Crop(ItemControl.IdFrame.Rectangle, ItemSettings.IdFrameMargin),
                Item.IsEnabled(ItemState.Item_Pending)
                    ? ColorExt.MakeTransparent(ItemSettings.IdFramePendingColor, alpha)
                    : ColorExt.MakeTransparent(ItemSettings.IdFrameColor, alpha));
        }

        protected override void DrawNameFrame(byte alpha)
        {
            base.DrawNameFrame(alpha);
            if (ItemData.Stopped)
            {
               this.FillNameFrameWith(ItemSettings.NameFrameStoppedColor, alpha); 
            }
        }

        private void DrawName(byte alpha)
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
                FontManager.DrawText(
                    ItemSettings.NameFont,
                    ItemData.Name,
                    this.NameLocation,
                    ColorExt.MakeTransparent(ItemSettings.NameColor, alpha),
                    ItemSettings.NameSize);
            }
        }

        #endregion Draw
    }
}