namespace MetaMind.Engine.Guis.Widgets.Views.Visuals
{
    using System;
    using Components.Fonts;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using Services;

    public class GradientViewVisual : ViewVisual
    {
        public GradientViewVisual(IView view) : base(view)
        {
        }

        private readonly int fadeSpeed = 255 * 10;

        protected int FocusAlpha { get; set; }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.DrawBegin(graphics, this.renderTarget);
            this.DrawItems(graphics, time, alpha);
            this.DrawComponents(graphics, time, Math.Max((byte)this.FocusAlpha, alpha));

            var mouse = Mouse.GetState();
            graphics.StringDrawer.DrawString(Font.UiRegular, "Should not be cropped by render target", new Vector2(mouse.X + 200, mouse.Y + 200), Color.White, 1f);

            this.DrawEnd(graphics);

            graphics.SpriteBatch.Draw(this.renderTarget, new Rectangle(0, 0, 900, 900), Color.White);
        }

        public override void Update(GameTime time)
        {
            base.Update(time);

            if (this.View[ViewState.View_Has_Focus]())
            {
                this.FocusAlpha += (int)(this.fadeSpeed * time.ElapsedGameTime.TotalSeconds);
                if (this.FocusAlpha > 255)
                {
                    this.FocusAlpha = 255;
                }
            }
            else
            {
                this.FocusAlpha -= (int)(this.fadeSpeed * time.ElapsedGameTime.TotalSeconds);
                if (this.FocusAlpha < 0)
                {
                    this.FocusAlpha = 0;
                }
            }
        }
    }
}