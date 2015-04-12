namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine;
    using MetaMind.Engine.Concepts;
    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Guis.Widgets.Items;

    using Microsoft.Xna.Framework;

    using Primtives2D;

    public class TraceItemGraphics : ViewItemGraphics
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
            get { return ExtPoint.ToVector2(ItemControl.ExperienceFrame.Center); }
        }

        protected virtual Vector2 HelpLocation
        {
            get { return this.NameLocation; }
        }

        protected override Vector2 IdCenter
        {
            get { return ExtPoint.ToVector2(ItemControl.IdFrame.Center); }
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

        public override void Draw(IGameGraphics gameGraphics, GameTime gameTime, byte alpha)
        {
            if (!ItemControl.Active && !Item.IsEnabled(ItemState.Item_Dragging))
            {
                return;
            }

            this.DrawNameFrame(gameGraphics, 255);

            this.FillNameFrameWith(gameGraphics, this.ItemSettings.NameFrameRunningColor, alpha);

            this.DrawName(255);
            this.DrawExperienceFrame(255);
            this.DrawExperience(255);
            this.DrawIdFrame(255);
            this.DrawId(TODO, 255);
        }

        public override void Update(GameTime gameTime)
        {
        }

        protected virtual void DrawExperience(byte alpha)
        {
            FontManager.DrawStringCenteredHV(
                ItemSettings.ExperienceFont,
                string.Format("{0:hh\\:mm\\:ss}", ((SynchronizationSpan)ItemData.Experience).Duration),
                this.ExperienceLocation,
                ExtColor.MakeTransparent(ItemSettings.ExperienceColor, alpha),
                ItemSettings.ExperienceSize);
        }

        protected void DrawExperienceFrame(byte alpha)
        {
            Primitives2D.FillRectangle(
                ScreenManager.SpriteBatch,
                ExtRectangle.Crop(ItemControl.ExperienceFrame.Rectangle, ItemSettings.ExperienceFrameMargin),
                ExtColor.MakeTransparent(ItemSettings.ExperienceFrameColor, alpha));
        }

        protected void DrawIdFrame(byte alpha)
        {
            Primitives2D.FillRectangle(
                ScreenManager.SpriteBatch,
                ExtRectangle.Crop(ItemControl.IdFrame.Rectangle, ItemSettings.IdFrameMargin),
                Item.IsEnabled(ItemState.Item_Pending)
                    ? ExtColor.MakeTransparent(ItemSettings.IdFramePendingColor, alpha)
                    : ExtColor.MakeTransparent(ItemSettings.IdFrameColor, alpha));
        }

        protected virtual void DrawName(byte alpha)
        {
            if (Item.IsEnabled(ItemState.Item_Pending))
            {
                FontManager.DrawString(
                    ItemSettings.HelpFont,
                    HelpInformation,
                    this.HelpLocation,
                    ExtColor.MakeTransparent(ItemSettings.HelpColor, alpha),
                    ItemSettings.HelpSize);
            }
            else
            {
                string text = FontManager.CropMonospacedString(
                    ItemData.Name,
                    ItemSettings.NameSize,
                    ItemSettings.NameFrameSize.X - ItemSettings.NameXLMargin * 2);
                
                FontManager.DrawMonospacedString(
                    ItemSettings.NameFont,
                    text,
                    this.NameLocation,
                    ExtColor.MakeTransparent(ItemSettings.NameColor, alpha),
                    ItemSettings.NameSize);
            }
        }

        #endregion Draw
    }
}