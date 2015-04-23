namespace MetaMind.Perseverance.Guis.Widgets
{
    using System;

    using MetaMind.Engine;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    using Primtives2D;

    public class TaskItemGraphics : ViewItemGraphics
    {
        private const string HelpInformation = "N D:one E:xp L:oad R:ationale";

        #region Constructors

        public TaskItemGraphics(IViewItem item)
            : base(item)
        {
        }

        #endregion Constructors

        #region Structure Position

        //protected override Vector2 IdCenterPosition()
        //{
        //    get { return ExtPoint.ToVector2(this.ItemControl.IdFrame.Center); }
        //}

        private Vector2 ExperienceCenter
        {
            get { return ExtPoint.ToVector2(this.ItemControl.ExperienceFrame.Center); }
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
            get { return ExtPoint.ToVector2(this.ItemControl.ProgressFrame.Center); }
        }

        private Vector2 RationaleCenter
        {
            get { return ExtPoint.ToVector2(this.ItemControl.RationaleFrame.Center); }
        }

        private Rectangle RandomHighlight(GameTime gameTime, int flashLength, Rectangle rectangle)
        {
            var thick = Perseverance.Session.Random.Next(flashLength);
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

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            if (!this.ItemControl.Active && !this.Item.IsEnabled(ItemState.Item_Dragging))
            {
                return;
            }

            this.DrawNameFrame(graphics, alpha);
            this.DrawName(graphics, alpha);
            this.DrawIdFrame(graphics, alpha);
            this.DrawId(graphics, alpha);
            this.DrawExperienceFrame(graphics, alpha);
            this.DrawExperience(graphics, alpha);
            this.DrawProgressFrame(graphics, alpha);
            this.DrawProgress(graphics, alpha);

            this.DrawSynchronization(graphics, time, alpha);
        }

        private void DrawExperience(IGameGraphicsService graphics, byte alpha)
        {
            //graphics.String.DrawStringCenteredHV(
            //    this.ItemSettings.IdFont,
            //    string.Format(
            //        "{0} : {1} : {2}",
            //        this.ItemData.Experience.Duration.TotalHours.ToString("F0"),
            //        this.ItemData.Experience.Duration.Minutes.ToString(),
            //        this.ItemData.Experience.Duration.Seconds.ToString()),
            //    this.ExperienceCenter,
            //    ExtColor.MakeTransparent(this.ItemSettings.ExperienceColor, alpha),
            //    this.ItemSettings.ExperienceSize);
        }

        private void DrawExperienceFrame(IGameGraphicsService graphics, byte alpha)
        {
            Primitives2D.FillRectangle(
                graphics.SpriteBatch,
                ExtRectangle.Crop(this.ItemControl.ExperienceFrame.Rectangle, this.ItemSettings.ExperienceFrameMargin),
                ExtColor.MakeTransparent(this.ItemSettings.ExperienceFrameColor, alpha));
        }

        private void DrawIdFrame(IGameGraphicsService graphics, byte alpha)
        {
            if (this.Item.IsEnabled(ItemState.Item_Pending))
            {
                Primitives2D.FillRectangle(
                    graphics.SpriteBatch,
                    ExtRectangle.Crop(this.ItemControl.IdFrame.Rectangle, this.ItemSettings.IdFrameMargin),
                    ExtColor.MakeTransparent(this.ItemSettings.IdFramePendingColor, alpha));
            }
            else
            {
                Primitives2D.FillRectangle(
                    graphics.SpriteBatch,
                    ExtRectangle.Crop(this.ItemControl.IdFrame.Rectangle, this.ItemSettings.IdFrameMargin),
                    ExtColor.MakeTransparent(this.ItemSettings.IdFrameColor, alpha));
            }
        }

        private void DrawName(IGameGraphicsService graphics, byte alpha)
        {
            //if (this.Item.IsEnabled(ItemState.Item_Pending))
            //{
            //    graphics.StringDrawer.DrawString(
            //        ItemSettings.HelpFont,
            //        HelpInformation,
            //        this.HelpLocation,
            //        ExtColor.MakeTransparent(this.ItemSettings.HelpColor, alpha),
            //        this.ItemSettings.HelpSize);
            //}
            //else
            //{
            //    string text = graphics.StringDrawer.CropMonospacedString(
            //        ItemData.Name,
            //        ItemSettings.NameSize,
            //        ItemSettings.NameFrameSize.X - ItemSettings.NameXLMargin * 2);

            //    graphics.StringDrawer.DrawMonospacedString(
            //        this.ItemSettings.NameFont,
            //        text,
            //        this.NameLocation,
            //        ExtColor.MakeTransparent(this.ItemSettings.NameColor, alpha),
            //        this.ItemSettings.NameSize);
            //}
        }

        private void DrawProgress(IGameGraphicsService graphics, byte alpha)
        {
            var progressRatio = MathHelper.Clamp((float)this.ItemData.Done / (float)(this.ItemData.Load + 0.1f), 0f, 1f);
            var progressBar   = ExtRectangle.Crop(this.ItemControl.ProgressFrame.Rectangle, this.ItemSettings.ProgressFrameMargin);
            progressBar.Width = (int)(progressBar.Width * progressRatio);

            Primitives2D.FillRectangle(
                graphics.SpriteBatch,
                progressBar,
                ExtColor.MakeTransparent(this.ItemSettings.ProgressBarColor, alpha));
            //graphics.StringDrawer.DrawStringCenteredHV(
            //    this.ItemSettings.ProgressFont,
            //    string.Format("{0} / {1} = {2}", this.ItemData.Done, this.ItemData.Load, progressRatio.ToString("F1")),
            //    this.ProgressLocation,
            //    ExtColor.MakeTransparent(this.ItemSettings.ProgressColor, alpha),
            //    this.ItemSettings.ProgressSize);
        }

        private void DrawProgressFrame(IGameGraphicsService graphics, byte alpha)
        {
            Primitives2D.FillRectangle(
                graphics.SpriteBatch,
                ExtRectangle.Crop(this.ItemControl.ProgressFrame.Rectangle, this.ItemSettings.ProgressFrameMargin),
                ExtColor.MakeTransparent(this.ItemSettings.ProgressFrameColor, alpha));
        }

        private void DrawSynchronization(IGameGraphicsService graphics, GameTime gameTime, byte alpha)
        {
            if (this.ItemData.Synchronizing)
            {
                Primitives2D.DrawRectangle(
                    graphics.SpriteBatch,
                    this.SinwaveHighlight(gameTime, 5, ExtRectangle.Crop(this.ItemControl.NameFrame.Rectangle, this.ItemSettings.NameFrameMargin)),
                    ExtColor.MakeTransparent(this.ItemSettings.NameFrameSynchronizationColor, alpha),
                    2f);
            }
        }

        #endregion Draw
    }
}