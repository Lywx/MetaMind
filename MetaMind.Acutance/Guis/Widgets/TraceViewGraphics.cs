namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine;
    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Services;
    using MetaMind.Runtime.Guis.Widgets;

    using Microsoft.Xna.Framework;

    using Primtives2D;

    public class TraceViewGraphics : TaskViewGraphics
    {
        public TraceViewGraphics(IView view, TraceViewSettings viewSettings, TraceItemSettings itemSettings)
            : base(view, viewSettings, itemSettings)
        {
        }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.DrawItems(graphics, time, (byte)this.FocusAlpha);
            this.DrawScrollBar(time);
        }

        protected override void DrawRegion(IGameGraphicsService graphics, GameTime gameTime, byte alpha)
        {
            Primitives2D.DrawRectangle(
                ScreenManager.SpriteBatch,
                ExtRectangle.Extend(this.ViewControl.Region.Frame.Rectangle, this.ViewSettings.BorderMargin),
                ExtColor.MakeTransparent(this.ViewSettings.HighlightColor, alpha),
                2f);
            Primitives2D.FillRectangle(
                ScreenManager.SpriteBatch,
                this.ViewControl.Region.Frame.Rectangle,
                ExtColor.MakeTransparent(this.ViewSettings.HighlightColor, alpha));
        }
    }
}