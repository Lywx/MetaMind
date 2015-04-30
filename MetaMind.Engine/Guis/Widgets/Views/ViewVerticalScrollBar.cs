// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewVerticalScrollBar.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Regions;
    using MetaMind.Engine.Guis.Widgets.Views.PointView;
    using MetaMind.Engine.Guis.Widgets.Views.PointView.ViewScroll;
    using MetaMind.Engine.Guis.Widgets.Visual;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class ViewVerticalScrollBar : GameVisualEntity
    {
        private readonly IPointViewVerticalScrollControl viewScroll;

        private readonly IPointView2DLogic viewLogic;

        private readonly IRegion viewRegion;

        private readonly PointView2DSettings viewSettings;

        private readonly ViewScrollbarSettings settings;

        private readonly Box shape; 
        
        private int brightness;
        
        public ViewVerticalScrollBar(PointView2DSettings viewSettings, IPointViewVerticalScrollControl viewScroll, IPointView2DLogic viewLogic, IRegion viewRegion, ViewScrollbarSettings scrollbarSettings)
        {
            this.viewScroll   = viewScroll;
            this.viewLogic    = viewLogic;
            this.viewRegion   = viewRegion;
            this.viewSettings = viewSettings;

            this.settings = scrollbarSettings;

            this.shape = new Box(() => this.Bounds, () => this.settings.Color, () => true);
        }

        private Rectangle Bounds
        {
            get
            {
                var distance = this.viewRegion.Height * (float)this.viewScroll.OffsetY
                               / (this.viewLogic.RowNum - this.viewSettings.RowNumDisplay)
                               * (1 - (float)this.viewSettings.RowNumDisplay / this.viewLogic.RowNum);

                // Top boundary reaches the top of the ViewRegion
                // Bottom boundary reaches the bottom of the ViewRegion
                return new Rectangle(
                    this.viewRegion.X + this.viewRegion.Width,
                    this.viewRegion.Y + (int)Math.Ceiling(distance),
                    this.settings.Width,
                    this.viewRegion.Height * this.viewSettings.RowNumDisplay / this.viewLogic.RowNum);
            }
        }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            if (this.viewLogic.RowNum > this.viewSettings.RowNumDisplay)
            {
                this.shape.Draw(graphics, time, Math.Min(alpha, (byte)this.brightness));
            }
        }

        public void Trigger()
        {
            this.brightness = this.settings.BrightnessMax;
        }

        public override void Update(GameTime time)
        {
            this.brightness -= this.settings.BrightnessDecreasingStep;
            
            if (this.brightness < 0)
            {
                this.brightness = 0;
            }
        }
    }
}