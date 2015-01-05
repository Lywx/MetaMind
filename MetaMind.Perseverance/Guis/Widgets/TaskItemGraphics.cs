namespace MetaMind.Perseverance.Guis.Widgets
{
    using System;

    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Guis.Widgets.Items;

    using Microsoft.Xna.Framework;

    using Primtives2D;

    public class TaskItemGraphics : ViewItemBasicGraphics
    {
        private const string HelpInformation = "N D:one E:xp L:oad R:ationale";

        #region Constructors

        public TaskItemGraphics(IViewItem item)
            : base(item)
        {
        }

        #endregion Constructors

        #region Structure Position

        protected override Vector2 IdCenter
        {
            get { return PointExt.ToVector2(this.ItemControl.IdFrame.Center); }
        }

        private Vector2 ExperienceCenter
        {
            get { return PointExt.ToVector2(this.ItemControl.ExperienceFrame.Center); }
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

        private Vector2 ProgressLocation
        {
            get { return PointExt.ToVector2(this.ItemControl.ProgressFrame.Center); }
        }

        private Vector2 RationaleCenter
        {
            get { return PointExt.ToVector2(this.ItemControl.RationaleFrame.Center); }
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
            this.DrawExperienceFrame(alpha);
            this.DrawExperience(alpha);
            this.DrawProgressFrame(alpha);
            this.DrawProgress(alpha);

            this.DrawSynchronization(gameTime, alpha);
        }

        private void DrawExperience(byte alpha)
        {
            FontManager.DrawCenteredText(
                this.ItemSettings.IdFont,
                string.Format(
                    "{0} : {1} : {2}",
                    this.ItemData.Experience.Duration.TotalHours.ToString("F0"),
                    this.ItemData.Experience.Duration.Minutes.ToString(),
                    this.ItemData.Experience.Duration.Seconds.ToString()),
                this.ExperienceCenter,
                ColorExt.MakeTransparent(this.ItemSettings.ExperienceColor, alpha),
                this.ItemSettings.ExperienceSize);
        }

        private void DrawExperienceFrame(byte alpha)
        {
            Primitives2D.FillRectangle(
                ScreenManager.SpriteBatch,
                RectangleExt.Crop(this.ItemControl.ExperienceFrame.Rectangle, this.ItemSettings.ExperienceFrameMargin),
                ColorExt.MakeTransparent(this.ItemSettings.ExperienceFrameColor, alpha));
        }

        private void DrawIdFrame(byte alpha)
        {
            if (this.Item.IsEnabled(ItemState.Item_Pending))
            {
                Primitives2D.FillRectangle(
                    ScreenManager.SpriteBatch,
                    RectangleExt.Crop(this.ItemControl.IdFrame.Rectangle, this.ItemSettings.IdFrameMargin),
                    ColorExt.MakeTransparent(this.ItemSettings.IdFramePendingColor, alpha));
            }
            else
            {
                Primitives2D.FillRectangle(
                    ScreenManager.SpriteBatch,
                    RectangleExt.Crop(this.ItemControl.IdFrame.Rectangle, this.ItemSettings.IdFrameMargin),
                    ColorExt.MakeTransparent(this.ItemSettings.IdFrameColor, alpha));
            }
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
                string text = FontManager.CropText(
                    ItemSettings.NameFont,
                    ItemData.Name,
                    ItemSettings.NameSize,
                    ItemSettings.NameFrameSize.X - ItemSettings.NameXLMargin * 2);

                FontManager.DrawText(
                    this.ItemSettings.NameFont,
                    text,
                    this.NameLocation,
                    ColorExt.MakeTransparent(this.ItemSettings.NameColor, alpha),
                    this.ItemSettings.NameSize);
            }
        }

        private void DrawProgress(byte alpha)
        {
            var progressRatio = MathHelper.Clamp((float)this.ItemData.Done / (float)(this.ItemData.Load + 0.1f), 0f, 1f);
            var progressBar   = RectangleExt.Crop(this.ItemControl.ProgressFrame.Rectangle, this.ItemSettings.ProgressFrameMargin);
            progressBar.Width = (int)(progressBar.Width * progressRatio);

            Primitives2D.FillRectangle(
                ScreenManager.SpriteBatch,
                progressBar,
                ColorExt.MakeTransparent(this.ItemSettings.ProgressBarColor, alpha));
            FontManager.DrawCenteredText(
                this.ItemSettings.ProgressFont,
                string.Format("{0} / {1} = {2}", this.ItemData.Done, this.ItemData.Load, progressRatio.ToString("F1")),
                this.ProgressLocation,
                ColorExt.MakeTransparent(this.ItemSettings.ProgressColor, alpha),
                this.ItemSettings.ProgressSize);
        }

        private void DrawProgressFrame(byte alpha)
        {
            Primitives2D.FillRectangle(
                ScreenManager.SpriteBatch,
                RectangleExt.Crop(this.ItemControl.ProgressFrame.Rectangle, this.ItemSettings.ProgressFrameMargin),
                ColorExt.MakeTransparent(this.ItemSettings.ProgressFrameColor, alpha));
        }

        private void DrawSynchronization(GameTime gameTime, byte alpha)
        {
            if (this.ItemData.Synchronizing)
            {
                Primitives2D.DrawRectangle(
                    ScreenManager.SpriteBatch,
                    this.SinwaveHighlight(gameTime, 5, RectangleExt.Crop(this.ItemControl.NameFrame.Rectangle, this.ItemSettings.NameFrameMargin)),
                    ColorExt.MakeTransparent(this.ItemSettings.NameFrameSynchronizationColor, alpha),
                    2f);
            }
        }

        #endregion Draw
    }
}