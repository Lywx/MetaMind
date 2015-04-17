// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PointViewScrollBar.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;

    using Microsoft.Xna.Framework;

    using Primtives2D;

    public class PointViewScrollBar : ViewVisualComponent
    {
        private readonly ViewScrollBarSettings settings;

        private int alpha;

        public PointViewScrollBar(IView view, PointViewSettings2D viewSettings, ICloneable itemSettings, ViewScrollBarSettings scrollBarSettings)
            : base(view, viewSettings, itemSettings)
        {
            this.settings = scrollBarSettings;
        }

        private Rectangle ScrollBarRectangle
        {
            get
            {
                var distance = this.ViewControl.Region.Height * (float)this.ViewControl.Scroll.YOffset
                               / (this.ViewControl.RowNum - this.ViewSettings.RowNumDisplay)
                               * (1 - (float)this.ViewSettings.RowNumDisplay / this.ViewControl.RowNum);

                // top boundary reaches the top of the ViewRegion
                // bottom boundary reaches the bottom of the ViewRegion
                return new Rectangle(
                    this.ViewControl.Region.X + this.ViewControl.Region.Width,
                    this.ViewControl.Region.Y + (int)Math.Ceiling(distance),
                    this.settings.Width,
                    this.ViewControl.Region.Height * this.ViewSettings.RowNumDisplay / this.ViewControl.RowNum);
            }
        }

        public override void Draw(IGameGraphics gameGraphics, GameTime gameTime, byte alpha)
        {
            if (this.ViewControl.RowNum > this.ViewSettings.RowNumDisplay)
            {
                Primitives2D.FillRectangle(
                    gameGraphics.Screen.SpriteBatch,
                    this.ScrollBarRectangle,
                    ExtColor.MakeTransparent(this.settings.Color, (byte)this.alpha));
            }
        }

        public void Trigger()
        {
            this.alpha = this.settings.BrightnessMax;
        }

        public override void Update(GameTime gameTime)
        {
            this.alpha -= this.settings.BrightnessTransitionRate;
            if (this.alpha < 0)
            {
                this.alpha = 0;
            }
        }
    }
}