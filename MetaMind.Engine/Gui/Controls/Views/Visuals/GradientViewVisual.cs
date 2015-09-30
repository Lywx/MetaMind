namespace MetaMind.Engine.Gui.Controls.Views.Visuals
{
    using System;
    using Microsoft.Xna.Framework;
    using Service;

    public class GradientViewVisual : ViewVisual
    {
        public GradientViewVisual(IView view) : base(view)
        {
        }

        private readonly int fadeSpeed = 255 * 10;

        protected int FocusAlpha { get; set; }

        public override void Draw(IMMEngineGraphicsService graphics, GameTime time, byte alpha)
        {
            this.DrawItems(graphics, time, alpha);
            this.DrawComponents(graphics, time, Math.Max((byte)this.FocusAlpha, alpha));
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