namespace MetaMind.Perseverance.Guis.Widgets
{
    using C3.Primtive2DXna;

    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Guis.Widgets.Views;

    using Microsoft.Xna.Framework;

    public class TaskViewGraphics : ViewBasicGraphics
    {
        public TaskViewGraphics(IView view, TaskViewSettings viewSettings, TaskItemSettings itemSettings)
            : base(view, viewSettings, itemSettings)
        {
        }

        protected int FocusAlpha { get; set; }

        public override void Draw(GameTime gameTime, byte alpha)
        {
            // draw active items
            this.DrawItems(gameTime, (byte)this.FocusAlpha);
            this.DrawRegion(gameTime, (byte)this.FocusAlpha);
            this.DrawScrollBar(gameTime);
        }

        public override void Update(GameTime gameTime)
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

        protected virtual void DrawRegion(GameTime gameTime, byte alpha)
        {
            Primitives2D.DrawRectangle(
                ScreenManager.SpriteBatch,
                RectangleExt.Extend(this.ViewControl.Region.Frame.Rectangle, this.ViewSettings.BorderMargin),
                ColorExt.MakeTransparent(this.ViewSettings.HighlightColor, alpha),
                2f);
            Primitives2D.FillRectangle(
                ScreenManager.SpriteBatch,
                this.ViewControl.Region.Frame.Rectangle,
                ColorExt.MakeTransparent(this.ViewSettings.HighlightColor, alpha));
        }

        protected virtual void DrawScrollBar(GameTime gameTime)
        {
            this.ViewControl.ScrollBar.Draw(gameTime);
        }
    }
}