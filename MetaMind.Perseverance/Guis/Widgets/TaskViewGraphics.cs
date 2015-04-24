namespace MetaMind.Runtime.Guis.Widgets
{
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    using Primtives2D;

    public class TaskViewGraphics : ViewGraphics
    {
        public TaskViewGraphics(IView view, TaskViewSettings viewSettings, TaskItemSettings itemSettings)
            : base(view, viewSettings, itemSettings)
        {
        }

        protected int FocusAlpha { get; set; }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            // draw active items
            this.DrawItems(graphics, time, (byte)this.FocusAlpha);
            this.DrawRegion(graphics, time, (byte)this.FocusAlpha);
            this.DrawScrollBar(time);
        }

        public override void Update(GameTime time)
        {
            if (this.View.IsEnabled(ViewState.View_Has_Focus))
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

        protected virtual void DrawRegion(IGameGraphicsService graphics, GameTime gameTime, byte alpha)
        {
            Primitives2D.DrawRectangle(
                graphics.SpriteBatch,
                ExtRectangle.Extend(this.ViewControl.Region.Frame.Rectangle, this.ViewSettings.BorderMargin),
                ExtColor.MakeTransparent(this.ViewSettings.HighlightColor, alpha),
                2f);
            Primitives2D.FillRectangle(
                graphics.SpriteBatch,
                this.ViewControl.Region.Frame.Rectangle,
                ExtColor.MakeTransparent(this.ViewSettings.HighlightColor, alpha));
        }

        protected virtual void DrawScrollBar(GameTime gameTime)
        {
            this.ViewControl.ScrollBar.Draw(gameTime);
        }
    }
}