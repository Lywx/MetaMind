namespace MetaMind.Engine.Gui.Controls.Views.Visuals
{
    using System;
    using Microsoft.Xna.Framework;
    using Services;

    public class GradientViewVisual : ViewVisual
    {
        public GradientViewVisual(IMMViewNode view) : base(view)
        {
        }

        private readonly int fadeSpeed = 255 * 10;

        protected int FocusOpacity { get; set; }

        public override void Draw(GameTime time)
        {
            this.DrawItems(graphics, time, alpha);
            this.DrawComponents(graphics, time, Math.Max((byte)this.FocusOpacity, alpha));
        }

        public override void Update(GameTime time)
        {
            base.Update(time);

            if (this.View[MMViewState.View_Has_Focus]())
            {
                this.FocusOpacity += (int)(this.fadeSpeed * time.ElapsedGameTime.TotalSeconds);
                if (this.FocusOpacity > 255)
                {
                    this.FocusOpacity = 255;
                }
            }
            else
            {
                this.FocusOpacity -= (int)(this.fadeSpeed * time.ElapsedGameTime.TotalSeconds);
                if (this.FocusOpacity < 0)
                {
                    this.FocusOpacity = 0;
                }
            }
        }
    }
}