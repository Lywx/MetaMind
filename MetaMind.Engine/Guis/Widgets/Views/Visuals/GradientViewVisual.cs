namespace MetaMind.Engine.Guis.Widgets.Views.Visuals
{
    using System;
    using Microsoft.Xna.Framework;

    using Services;

    public class GradientViewVisual : ViewVisual
    {
        private readonly int fadeSpeed = 255 * 10;

        public GradientViewVisual(IView view) : base(view)
        {
        }

        protected int FocusAlpha { get; set; }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.DrawItems(graphics, time, alpha);
            this.DrawComponents(graphics, time, Math.Min((byte)this.FocusAlpha, alpha));
        }

        public override void Update(GameTime time)
        {
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