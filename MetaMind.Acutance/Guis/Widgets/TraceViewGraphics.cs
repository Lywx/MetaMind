namespace MetaMind.Acutance.Guis.Widgets
{
    using C3.Primtive2DXna;

    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Guis.Elements.Views;
    using MetaMind.Perseverance.Guis.Widgets.Tasks.Views;

    using Microsoft.Xna.Framework;

    public class TraceViewGraphics : TaskViewGraphics
    {
        public TraceViewGraphics(IView view, TraceViewSettings viewSettings, TraceItemSettings itemSettings)
            : base(view, viewSettings, itemSettings)
        {
        }

        public override void Draw(GameTime gameTime, byte alpha)
        {
            this.DrawItems(gameTime, (byte)this.FocusAlpha);
            this.DrawRegion(gameTime, alpha);
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