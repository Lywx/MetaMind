using C3.Primtive2DXna;
using MetaMind.Engine.Extensions;

using Microsoft.Xna.Framework;
using System;

namespace MetaMind.Perseverance.Guis.Widgets.Tasks.Items
{
    using MetaMind.Engine.Guis.Elements.Items;
    using MetaMind.Engine.Guis.Elements.ViewItems;

    public class TaskItemGraphics : ViewItemBasicGraphics
    {
        private const string HelpInformation = "N-ame D-one E-xperience L-oad";

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
                    ItemControl.NameFrame.Location.Y + ItemSettings.NameYTMargin
                    );
            }
        }

        private Vector2 ProgressLocation
        {
            get { return PointExt.ToVector2(ItemControl.ProgressFrame.Center); }
        }

        private Rectangle RandomHighlight(GameTime gameTime, int flashLength, Rectangle rectangle)
        {
            var thick = Perseverance.Adventure.Random.Next(flashLength);
            return new Rectangle(
                rectangle.X - thick,
                rectangle.Y - thick,
                rectangle.Width + thick * 2,
                rectangle.Height + thick * 2
                );
        }

        private Rectangle SinwaveHighlight(GameTime gameTime, int flashLength, Rectangle rectangle)
        {
            var thick = (int)(flashLength * Math.Abs(Math.Sin(gameTime.TotalGameTime.TotalSeconds * 30)));
            return new Rectangle(
                rectangle.X - thick,
                rectangle.Y - thick,
                rectangle.Width + thick * 2,
                rectangle.Height + thick * 2
                );
        }

        #endregion Structure Position

        #region Update

        public override void Update(GameTime gameTime)
        {
        }

        #endregion Update

        #region Draw

        public override void Draw(GameTime gameTime, byte alpha)
        {
            if (!ItemControl.Active && !Item.IsEnabled(ItemState.Item_Dragging))
            {
                return;
            }

            DrawNameFrame(alpha);
            DrawName(alpha);
            DrawIdFrame(alpha);
            DrawId(alpha);
            DrawExperienceFrame(alpha);
            DrawExperience(alpha);
            DrawProgressFrame(alpha);
            DrawProgress(alpha);
            //DrawSynchronization( gameTime );
        }

        private void DrawExperience(byte alpha)
        {
            FontManager.DrawCenteredText(ItemSettings.IdFont, string.Format("Hs: {0}", ItemData.Experience.Duration.TotalHours.ToString("F0")), ExperienceCenter, ColorExt.MakeTransparent(ItemSettings.ExperienceColor, alpha), ItemSettings.ExperienceSize);
        }

        private void DrawExperienceFrame(byte alpha)
        {
            Primitives2D.FillRectangle(ScreenManager.SpriteBatch, RectangleExt.Crop(ItemControl.ExperienceFrame.Rectangle, ItemSettings.ExperienceFrameMargin), ColorExt.MakeTransparent(ItemSettings.ExperienceFrameColor, alpha));
        }

        private void DrawIdFrame(byte alpha)
        {
            if (Item.IsEnabled(ItemState.Item_Pending))
            {
                Primitives2D.FillRectangle(ScreenManager.SpriteBatch, RectangleExt.Crop(ItemControl.IdFrame.Rectangle, ItemSettings.IdFrameMargin), ColorExt.MakeTransparent(ItemSettings.IdFramePendingColor, alpha));
            }
            else if (!ItemData.Rationalized)
            {
                Primitives2D.FillRectangle(ScreenManager.SpriteBatch, RectangleExt.Crop(ItemControl.IdFrame.Rectangle, ItemSettings.IdFrameMargin), ColorExt.MakeTransparent(ItemSettings.IdFrameNotDocumentedColor, alpha));
            }
            else
            {
                Primitives2D.FillRectangle(ScreenManager.SpriteBatch, RectangleExt.Crop(ItemControl.IdFrame.Rectangle, ItemSettings.IdFrameMargin), ColorExt.MakeTransparent(ItemSettings.IdFrameColor, alpha));
            }
        }

        private void DrawName(byte alpha)
        {
            if (Item.IsEnabled(ItemState.Item_Pending))
            {
                FontManager.DrawText(ItemSettings.HelpFont, HelpInformation, HelpLocation, ColorExt.MakeTransparent(ItemSettings.HelpColor, alpha), ItemSettings.HelpSize);
            }
            else
            {
                FontManager.DrawText(ItemSettings.NameFont, ItemData.Name, NameLocation, ColorExt.MakeTransparent(ItemSettings.NameColor, alpha), ItemSettings.NameSize);
            }
        }

        private void DrawNameFrame(byte alpha)
        {
            if (Item.IsEnabled(ItemState.Item_Mouse_Over) && Item.IsEnabled(ItemState.Item_Editing))
            {
                FillNameFrameWith(ItemSettings.NameFrameModificationColor);
                DrawNameFrameWith(ItemSettings.NameFrameMouseOverColor);
            }
            else if (!Item.IsEnabled(ItemState.Item_Mouse_Over) && Item.IsEnabled(ItemState.Item_Editing))
            {
                FillNameFrameWith(ItemSettings.NameFrameModificationColor);
            }
            else if (Item.IsEnabled(ItemState.Item_Mouse_Over) && Item.IsEnabled(ItemState.Item_Selected))
            {
                FillNameFrameWith(ItemSettings.NameFrameSelectionColor);
                DrawNameFrameWith(ItemSettings.NameFrameMouseOverColor);
            }
            else if (Item.IsEnabled(ItemState.Item_Mouse_Over) && !Item.IsEnabled(ItemState.Item_Selected))
            {
                DrawNameFrameWith(ItemSettings.NameFrameMouseOverColor);
            }
            else if (Item.IsEnabled(ItemState.Item_Selected))
            {
                FillNameFrameWith(ItemSettings.NameFrameSelectionColor);
            }
            else
            {
                FillNameFrameWith(ItemSettings.NameFrameRegularColor);
            }
        }

        private void DrawNameFrameWith(Color color)
        {
            Primitives2D.DrawRectangle(ScreenManager.SpriteBatch, RectangleExt.Crop(ItemControl.NameFrame.Rectangle, ItemSettings.NameFrameMargin), color, 1f);
        }

        private void DrawProgress(byte alpha)
        {
            var progressRatio = MathHelper.Clamp((float)ItemData.Done / (float)(ItemData.Load + 0.1f), 0f, 1f);
            var progressBar   = RectangleExt.Crop(ItemControl.ProgressFrame.Rectangle, ItemSettings.ProgressFrameMargin);
            progressBar.Width = (int)(progressBar.Width * progressRatio);

            Primitives2D.FillRectangle(ScreenManager.SpriteBatch, progressBar, ColorExt.MakeTransparent(ItemSettings.ProgressBarColor, alpha));
            FontManager.DrawCenteredText(ItemSettings.ProgressFont, string.Format("{0} / {1} = {2}", ItemData.Done, ItemData.Load, progressRatio.ToString("F1")), ProgressLocation, ColorExt.MakeTransparent(ItemSettings.ProgressColor, alpha), ItemSettings.ProgressSize);
        }

        private void DrawProgressFrame(byte alpha)
        {
            Primitives2D.FillRectangle(ScreenManager.SpriteBatch, RectangleExt.Crop(ItemControl.ProgressFrame.Rectangle, ItemSettings.ProgressFrameMargin), ColorExt.MakeTransparent(ItemSettings.ProgressFrameColor, alpha));
        }

        private void DrawSynchronization(GameTime gameTime, byte alpha)
        {
            if (ItemData.IsRunning)
            {
                Primitives2D.DrawRectangle(ScreenManager.SpriteBatch, SinwaveHighlight(gameTime, 10, RectangleExt.Crop(ItemControl.NameFrame.Rectangle, ItemSettings.NameFrameMargin)), ColorExt.MakeTransparent(ItemSettings.NameFrameSynchronizationColor, alpha), 2f);
            }
        }

        private void FillNameFrameWith(Color color)
        {
            Primitives2D.FillRectangle(ScreenManager.SpriteBatch, RectangleExt.Crop(ItemControl.NameFrame.Rectangle, ItemSettings.NameFrameMargin), color);
        }

        #endregion Draw
    }
}