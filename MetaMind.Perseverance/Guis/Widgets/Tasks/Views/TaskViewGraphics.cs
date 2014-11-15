using C3.Primtive2DXna;
using MetaMind.Engine.Extensions;
using MetaMind.Perseverance.Guis.Widgets.Tasks.Items;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.Tasks.Views
{
    using MetaMind.Engine.Guis.Elements.Views;

    public class TaskViewGraphics : ViewBasicGraphics
    {
        private int frameAlpha;

        public TaskViewGraphics(IView view, TaskViewSettings viewSettings, TaskItemSettings itemSettings)
            : base(view, viewSettings, itemSettings)
        {
        }

        public override void Draw(GameTime gameTime, byte alpha)
        {
            // draw active items
            base.Draw(gameTime, (byte)frameAlpha);

            DrawRegion(gameTime);
            DrawScrollBar(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            if (View.IsEnabled(ViewState.View_Has_Focus))
            {
                frameAlpha += 15;
                if (frameAlpha > 255)
                {
                    frameAlpha = 255;
                }
            }
            else
            {
                frameAlpha -= 15;
                if (frameAlpha < 0)
                {
                    frameAlpha = 0;
                }
            }
        }

        private void DrawRegion(GameTime gameTime)
        {
            Primitives2D.DrawRectangle(
                ScreenManager.SpriteBatch,
                RectangleExt.Extend(ViewControl.Region.Frame.Rectangle, ViewSettings.BorderMargin),
                ColorExt.MakeTransparent(ViewSettings.HighlightColor, (byte)this.frameAlpha),
                2f);
            Primitives2D.FillRectangle(
                ScreenManager.SpriteBatch,
                ViewControl.Region.Frame.Rectangle,
                ColorExt.MakeTransparent(ViewSettings.HighlightColor, (byte)this.frameAlpha));
        }

        private void DrawScrollBar(GameTime gameTime)
        {
            ViewControl.ScrollBar.Draw(gameTime);
        }
    }
}