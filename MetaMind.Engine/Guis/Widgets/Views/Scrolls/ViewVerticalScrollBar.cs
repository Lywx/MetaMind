// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewVerticalScrollbar.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Views.Scrolls
{
    using System;

    using Microsoft.Xna.Framework;

    using Layouts;
    using Services;
    using Settings;
    using Widgets.Regions;
    using Widgets.Visuals;

    public class ViewVerticalScrollbar : GameControllableEntity, IViewVerticalScrollbar
    {
        private readonly IPointViewVerticalScrollController viewScroll;

        private readonly IPointViewVerticalLayout viewLayout;

        private readonly IRegion viewRegion;

        private readonly IPointViewVerticalSettings viewSettings;

        private readonly ViewScrollbarSettings scrollbarSettings;

        private readonly Box scrollbarShape; 
        
        private int scrollbarBrightness;
        
        public ViewVerticalScrollbar(IPointViewVerticalSettings viewSettings, IPointViewVerticalScrollController viewScroll, IPointViewVerticalLayout viewLayout, IRegion viewRegion, ViewScrollbarSettings scrollbarSettings)
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
                var distance = this.viewRegion.Height * (float)this.viewScroll.RowOffset
                               / (this.viewLayout.RowNum - this.viewSettings.ViewRowDisplay)
                               * (1 - (float)this.viewSettings.ViewRowDisplay / this.viewLayout.RowNum);

                // Top boundary reaches the top of the ViewRegion
                // Bottom boundary reaches the bottom of the ViewRegion
                return new Rectangle(
                    this.viewRegion.X + this.viewRegion.Width,
                    this.viewRegion.Y + (int)Math.Ceiling(distance),
                    this.scrollbarSettings.Width,
                    this.viewRegion.Height * this.viewSettings.ViewRowDisplay / this.viewLayout.RowNum);
            }
        }

        #region Draw

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            if (this.viewLayout.RowNum > this.viewSettings.ViewRowDisplay)
            {
                this.scrollbarShape.Draw(graphics, time, Math.Min(alpha, (byte)this.scrollbarBrightness));
            }
        }


        #endregion

        #region Update

        public override void Update(GameTime time)
        {
            this.scrollbarBrightness -= (int)(this.scrollbarSettings.BrightnessFadeSpeed * time.ElapsedGameTime.TotalSeconds);

            if (this.scrollbarBrightness < 0)
            {
                this.scrollbarBrightness = 0;
            }
        }

        #endregion

        #region Operations

        public void Trigger()
        {
            this.scrollbarBrightness = this.scrollbarSettings.BrightnessMax;
        }

        #endregion
    }
}