// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MotivationViewGraphics.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance.Guis.Widgets
{
    using MetaMind.Engine;
    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Guis.Widgets.Views;

    using Microsoft.Xna.Framework;

    using Primtives2D;

    public class MotivationViewGraphics : ViewGraphics
    {
        private int frameAlpha;

        public MotivationViewGraphics(IView view, MotivationViewSettings viewSettings, MotivationItemSettings itemSettings)
            : base(view, viewSettings, itemSettings)
        {
        }

        public override void Draw(IGameGraphics gameGraphics, GameTime gameTime, byte alpha)
        {
            base.Draw(gameGraphics, gameTime, alpha);

            this.DrawRegion(gameGraphics, gameTime);
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

        private void DrawRegion(IGameGraphics gameGraphics, GameTime gameTime)
        {
            Primitives2D.FillRectangle(
                gameGraphics.Screens.SpriteBatch,
                this.ViewControl.Region.Frame.Rectangle,
                ExtColor.MakeTransparent(this.ViewSettings.HighlightColor, (byte)this.frameAlpha));
        }
    }
}