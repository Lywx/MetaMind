namespace MetaMind.Perseverance.Guis.Widgets.Tasks.Items
{
    using System;
    using System.Globalization;

    using C3.Primtive2DXna;

    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Guis.Elements.Items;
    using MetaMind.Engine.Guis.Elements.ViewItems;

    using Microsoft.Xna.Framework;

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
            get { return PointExt.ToVector2(ItemControl.IdFrame.Center); }
        }

        private Vector2 ExperienceCenter
        {
            get { return PointExt.ToVector2(ItemControl.ExperienceFrame.Center); }
        }

        private Vector2 HelpLocation
        {
            get { return NameLocation; }
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

        private Vector2 ProgressLocation
        {
            get { return PointExt.ToVector2(ItemControl.ProgressFrame.Center); }
        }

        private Vector2 RationaleCenter
        {
            get { return PointExt.ToVector2(ItemControl.RationaleFrame.Center); }
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
            if (!ItemControl.Active && !Item.IsEnabled(ItemState.Item_Dragging))
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
            this.DrawRationaleFrame(alpha);
            this.DrawRationale(alpha);

            this.DrawSynchronization(gameTime, alpha);
        }

        private void DrawExperience(byte alpha)
        {
            FontManager.DrawCenteredText(
                ItemSettings.IdFont,
                string.Format(
                    "{0} : {1} : {2}",
                    ItemData.Experience.Duration.TotalHours.ToString("F0"),
                    ItemData.Experience.Duration.Minutes.ToString(),
                    ItemData.Experience.Duration.Seconds.ToString()),
                this.ExperienceCenter,
                ColorExt.MakeTransparent(ItemSettings.ExperienceColor, alpha),
                ItemSettings.ExperienceSize);
        }

        private void DrawExperienceFrame(byte alpha)
        {
            Primitives2D.FillRectangle(
                ScreenManager.SpriteBatch,
                RectangleExt.Crop(ItemControl.ExperienceFrame.Rectangle, ItemSettings.ExperienceFrameMargin),
                ColorExt.MakeTransparent(ItemSettings.ExperienceFrameColor, alpha));
        }

        private void DrawIdFrame(byte alpha)
        {
            if (Item.IsEnabled(ItemState.Item_Pending))
            {
                Primitives2D.FillRectangle(
                    ScreenManager.SpriteBatch,
                    RectangleExt.Crop(ItemControl.IdFrame.Rectangle, ItemSettings.IdFrameMargin),
                    ColorExt.MakeTransparent(ItemSettings.IdFramePendingColor, alpha));
            }
            else if (ItemData.RationaleScale == 0)
            {
                Primitives2D.FillRectangle(
                    ScreenManager.SpriteBatch,
                    RectangleExt.Crop(ItemControl.IdFrame.Rectangle, ItemSettings.IdFrameMargin),
                    ColorExt.MakeTransparent(ItemSettings.IdFrameNotDocumentedColor, alpha));
            }
            else
            {
                Primitives2D.FillRectangle(
                    ScreenManager.SpriteBatch,
                    RectangleExt.Crop(ItemControl.IdFrame.Rectangle, ItemSettings.IdFrameMargin),
                    ColorExt.MakeTransparent(ItemSettings.IdFrameColor, alpha));
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


        private void DrawNameFrame(byte alpha)
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

        private void DrawNameFrameWith(Color color, byte alpha)
        {
            Primitives2D.DrawRectangle(
                ScreenManager.SpriteBatch,
                RectangleExt.Crop(ItemControl.NameFrame.Rectangle, ItemSettings.NameFrameMargin),
                color.MakeTransparent(alpha),
                1f);
        }

        private void DrawProgress(byte alpha)
        {
            var progressRatio = MathHelper.Clamp((float)ItemData.Done / (float)(ItemData.Load + 0.1f), 0f, 1f);
            var progressBar   = RectangleExt.Crop(ItemControl.ProgressFrame.Rectangle, ItemSettings.ProgressFrameMargin);
            progressBar.Width = (int)(progressBar.Width * progressRatio);

            Primitives2D.FillRectangle(
                ScreenManager.SpriteBatch,
                progressBar,
                ColorExt.MakeTransparent(ItemSettings.ProgressBarColor, alpha));
            FontManager.DrawCenteredText(
                ItemSettings.ProgressFont,
                string.Format("{0} / {1} = {2}", ItemData.Done, ItemData.Load, progressRatio.ToString("F1")),
                ProgressLocation,
                ColorExt.MakeTransparent(ItemSettings.ProgressColor, alpha),
                ItemSettings.ProgressSize);
        }

        private void DrawProgressFrame(byte alpha)
        {
            Primitives2D.FillRectangle(
                ScreenManager.SpriteBatch,
                RectangleExt.Crop(ItemControl.ProgressFrame.Rectangle, ItemSettings.ProgressFrameMargin),
                ColorExt.MakeTransparent(ItemSettings.ProgressFrameColor, alpha));
        }

        private void DrawRationale(byte alpha)
        {
            FontManager.DrawCenteredText(
                ItemSettings.RationaleFont, 
                ItemData.RationaleScale.ToString(new CultureInfo("en-US")),
                this.RationaleCenter,
                ColorExt.MakeTransparent(ItemSettings.RationaleColor, alpha),
                ItemSettings.IdSize);
        }

        private void DrawRationaleFrame(byte alpha)
        {
            Primitives2D.FillRectangle(
                ScreenManager.SpriteBatch,
                RectangleExt.Crop(ItemControl.RationaleFrame.Rectangle, ItemSettings.RationaleFrameMargin),
                ColorExt.MakeTransparent(ItemSettings.RationaleFrameColor, alpha));
        }

        private void DrawSynchronization(GameTime gameTime, byte alpha)
        {
            if (ItemData.Synchronizing)
            {
                Primitives2D.DrawRectangle(
                    ScreenManager.SpriteBatch,
                    SinwaveHighlight(
                        gameTime,
                        5,
                        RectangleExt.Crop(ItemControl.NameFrame.Rectangle, ItemSettings.NameFrameMargin)),
                    ColorExt.MakeTransparent(ItemSettings.NameFrameSynchronizationColor, alpha),
                    2f);
            }
        }

        private void FillNameFrameWith(Color color, byte alpha)
        {
            Primitives2D.FillRectangle(
                ScreenManager.SpriteBatch,
                RectangleExt.Crop(ItemControl.NameFrame.Rectangle, ItemSettings.NameFrameMargin),
                color.MakeTransparent(alpha));
        }

        #endregion Draw
    }
}