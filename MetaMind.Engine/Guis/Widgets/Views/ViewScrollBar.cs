// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewScrollBar.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;

    using C3.Primtive2DXna;

    using MetaMind.Engine.Extensions;

    using Microsoft.Xna.Framework;

    public class ViewScrollBar : VisualViewComponent
    {
        private readonly ViewScrollBarSettings settings;

        private int alpha;

        public ViewScrollBar(IView view, ViewSettings2D viewSettings, ICloneable itemSettings, ViewScrollBarSettings scrollBarSettings)
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

        public override void Draw(GameTime gameTime)
        {
            if (this.ViewControl.RowNum > this.ViewSettings.RowNumDisplay)
            {
                Primitives2D.FillRectangle(
                    ScreenManager.SpriteBatch,
                    this.ScrollBarRectangle,
                    ColorExt.MakeTransparent(this.settings.Color, (byte)this.alpha));
            }
        }

        public void Trigger()
        {
            this.alpha = this.settings.BrightnessMax;
        }

        public override void UpdateStructure(GameTime gameTime)
        {
            this.alpha -= this.settings.BrightnessTransitionRate;
            if (this.alpha < 0)
            {
                this.alpha = 0;
            }
        }
    }
}