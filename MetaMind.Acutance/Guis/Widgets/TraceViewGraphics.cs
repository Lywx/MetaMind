namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Perseverance.Guis.Widgets;

    using Microsoft.Xna.Framework;

    using Primtives2D;

    public class TraceViewGraphics : TaskViewGraphics
    {
        public TraceViewGraphics(IView view, TraceViewSettings viewSettings, TraceItemSettings itemSettings)
            : base(view, viewSettings, itemSettings)
        {
        }

        public override void Draw(GameTime gameTime, byte alpha)
        {
            this.DrawItems(gameTime, (byte)this.FocusAlpha);
            this.DrawScrollBar(gameTime);
        }

        protected override void DrawRegion(GameTime gameTime, byte alpha)
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
    }
}