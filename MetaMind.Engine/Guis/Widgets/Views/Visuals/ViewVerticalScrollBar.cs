// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewVerticalScrollBar.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Views.Visuals
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Regions;
    using MetaMind.Engine.Guis.Widgets.Views.Layouts;
    using MetaMind.Engine.Guis.Widgets.Views.Logic;
    using MetaMind.Engine.Guis.Widgets.Views.Scrolls;
    using MetaMind.Engine.Guis.Widgets.Views.Settings;
    using MetaMind.Engine.Guis.Widgets.Visuals;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class ViewVerticalScrollBar : GameVisualEntity
    {
        private readonly IPointViewVerticalScrollController viewScroll;

        private readonly IPointView2DLayout viewLayout;

        private readonly IRegion viewRegion;

        private readonly PointView2DSettings viewSettings;

        private readonly ViewScrollbarSettings scrollbarSettings;

        private readonly Box scrollbarShape; 
        
        private int scrollbarBrightness;
        
        public ViewVerticalScrollBar(PointView2DSettings viewSettings, IPointViewVerticalScrollController viewScroll, IPointView2DLayout viewLayout, IRegion viewRegion, ViewScrollbarSettings scrollbarSettings)
        {
            if (viewSettings == null)
            {
                throw new ArgumentNullException("viewSettings");
            }

            if (viewScroll == null)
            {
                throw new ArgumentNullException("viewScroll");
            }

            if (viewRegion == null)
            {
                throw new ArgumentNullException("viewRegion");
            }

            if (viewLayout == null)
            {
                throw new ArgumentNullException("viewLayout");
            }

            this.viewScroll   = viewScroll;
            this.viewRegion   = viewRegion;
            this.viewSettings = viewSettings;
            this.viewLayout   = viewLayout;

            if (scrollbarSettings == null)
            {
                throw new ArgumentNullException("scrollbarSettings");
            }

            this.scrollbarSettings = scrollbarSettings;
            this.scrollbarShape    = new Box(() => this.Bounds, () => this.scrollbarSettings.Color, () => true);
        }

        private Rectangle Bounds
        {
            get
            {
                var distance = this.viewRegion.Height * (float)this.viewScroll.OffsetY
                               / (this.viewLayout.RowNum - this.viewSettings.RowNumDisplay)
                               * (1 - (float)this.viewSettings.RowNumDisplay / this.viewLayout.RowNum);

                // Top boundary reaches the top of the ViewRegion
                // Bottom boundary reaches the bottom of the ViewRegion
                return new Rectangle(
                    this.viewRegion.X + this.viewRegion.Width,
                    this.viewRegion.Y + (int)Math.Ceiling(distance),
                    this.scrollbarSettings.Width,
                    this.viewRegion.Height * this.viewSettings.RowNumDisplay / this.viewLayout.RowNum);
            }
        }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            if (this.viewLayout.RowNum > this.viewSettings.RowNumDisplay)
            {
                this.scrollbarShape.Draw(graphics, time, Math.Min(alpha, (byte)this.scrollbarBrightness));
            }
        }

        public void Trigger()
        {
            this.scrollbarBrightness = this.scrollbarSettings.BrightnessMax;
        }

        public override void Update(GameTime time)
        {
            this.scrollbarBrightness -= this.scrollbarSettings.BrightnessDecreasingStep;
            
            if (this.scrollbarBrightness < 0)
            {
                this.scrollbarBrightness = 0;
            }
        }
    }
}