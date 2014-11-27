namespace MetaMind.Acutance.Guis.Widgets
{
    using C3.Primtive2DXna;

    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Guis.Elements.Views;
    using MetaMind.Perseverance.Guis.Widgets.Tasks.Items;
    using MetaMind.Perseverance.Guis.Widgets.Tasks.Views;

    using Microsoft.Xna.Framework;

    public class TraceViewGraphics : ViewBasicGraphics
    {
        private int frameAlpha;

        public TraceViewGraphics(IView view, TraceViewSettings viewSettings, TraceItemSettings itemSettings)
            : base(view, viewSettings, itemSettings)
        {
        }

        public override void Draw(GameTime gameTime, byte alpha)
        {
            // draw active items
            base.Draw(gameTime, (byte)this.frameAlpha);

            this.DrawRegion(gameTime);
            this.DrawScrollBar(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            if (this.View.IsEnabled(ViewState.View_Has_Focus))
            {
                this.frameAlpha += 15;
                if (this.frameAlpha > 255)
                {
                    this.frameAlpha = 255;
                }
            }
            else
            {
                this.frameAlpha -= 15;
                if (this.frameAlpha < 0)
                {
                    this.frameAlpha = 0;
                }
            }
        }

        private void DrawRegion(GameTime gameTime)
        {
            Primitives2D.DrawRectangle(
                ScreenManager.SpriteBatch,
                RectangleExt.Extend(this.ViewControl.Region.Frame.Rectangle, this.ViewSettings.BorderMargin),
                ColorExt.MakeTransparent(this.ViewSettings.HighlightColor, (byte)this.frameAlpha),
                2f);
            Primitives2D.FillRectangle(
                ScreenManager.SpriteBatch,
                this.ViewControl.Region.Frame.Rectangle,
                ColorExt.MakeTransparent(this.ViewSettings.HighlightColor, (byte)this.frameAlpha));
        }

        private void DrawScrollBar(GameTime gameTime)
        {
            this.ViewControl.ScrollBar.Draw(gameTime);
        }
    }
}