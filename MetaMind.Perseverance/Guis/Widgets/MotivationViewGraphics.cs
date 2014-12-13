// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MotivationViewGraphics.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance.Guis.Widgets
{
    using C3.Primtive2DXna;

    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Guis.Widgets.Views;

    using Microsoft.Xna.Framework;

    public class MotivationViewGraphics : ViewBasicGraphics
    {
        private int frameAlpha;

        public MotivationViewGraphics(IView view, MotivationViewSettings viewSettings, MotivationItemSettings itemSettings)
            : base(view, viewSettings, itemSettings)
        {
        }

        public override void Draw(GameTime gameTime, byte alpha)
        {
            base.Draw(gameTime, alpha);

            this.DrawRegion(gameTime);
        }

        public override void UpdateStructure(GameTime gameTime)
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
            Primitives2D.FillRectangle(
                ScreenManager.SpriteBatch,
                this.ViewControl.Region.Frame.Rectangle,
                ColorExt.MakeTransparent(this.ViewSettings.HighlightColor, (byte)this.frameAlpha));
        }
    }
}