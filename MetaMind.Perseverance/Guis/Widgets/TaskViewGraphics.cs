namespace MetaMind.Perseverance.Guis.Widgets
{
    using MetaMind.Engine;
    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Guis.Widgets.Views;

    using Microsoft.Xna.Framework;

    using Primtives2D;

    public class TaskViewGraphics : ViewGraphics
    {
        public TaskViewGraphics(IView view, TaskViewSettings viewSettings, TaskItemSettings itemSettings)
            : base(view, viewSettings, itemSettings)
        {
        }

        protected int FocusAlpha { get; set; }

        public override void Draw(IGameGraphics gameGraphics, GameTime gameTime, byte alpha)
        {
            // draw active items
            this.DrawItems(gameGraphics, gameTime, (byte)this.FocusAlpha);
            this.DrawRegion(gameGraphics, gameTime, (byte)this.FocusAlpha);
            this.DrawScrollBar(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            if (View.IsEnabled(ViewState.View_Has_Focus))
            {
                this.FocusAlpha += 15;
                if (this.FocusAlpha > 255)
                {
                    this.FocusAlpha = 255;
                }
            }
            else
            {
                this.FocusAlpha -= 15;
                if (this.FocusAlpha < 0)
                {
                    this.FocusAlpha = 0;
                }
            }
        }

        protected virtual void DrawRegion(IGameGraphics gameGraphics, GameTime gameTime, byte alpha)
        {
            Primitives2D.DrawRectangle(
                gameGraphics.Screen.SpriteBatch,
                ExtRectangle.Extend(ViewControl.Region.Frame.Rectangle, ViewSettings.BorderMargin),
                ExtColor.MakeTransparent(ViewSettings.HighlightColor, alpha),
                2f);
            Primitives2D.FillRectangle(
                gameGraphics.Screen.SpriteBatch,
                ViewControl.Region.Frame.Rectangle,
                ExtColor.MakeTransparent(ViewSettings.HighlightColor, alpha));
        }

        protected virtual void DrawScrollBar(GameTime gameTime)
        {
            ViewControl.ScrollBar.Draw(gameTime);
        }
    }
}