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

    using Elements;
    using Layouts;
    using Services;
    using Settings;
    using Widgets.Regions;

    public class ViewVerticalScrollbar : DraggableFrame, IViewVerticalScrollbar
    {
        private readonly IPointViewVerticalScrollController viewScroll;

        private readonly IPointViewVerticalLayout viewLayout;

        private readonly IRegion viewRegion;

        private readonly IPointViewVerticalSettings viewSettings;

        private readonly ViewScrollbarSettings scrollbarSettings;

        private ViewVerticalScrollbarVisual scrollbarVisual;

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

            this.scrollbarVisual = new ViewVerticalScrollbarVisual(this);

            if (scrollbarSettings == null)
            {
                throw new ArgumentNullException("scrollbarSettings");
            }

            this.scrollbarSettings = scrollbarSettings;
        }

        public ViewScrollbarSettings ScrollbarSettings
        {
            get { return this.scrollbarSettings; }
        }

        #region Positions

        private bool CanDisplay()
        {
            return this.CanDisplay(this.viewSettings.ViewRowDisplay, this.viewLayout.RowNum);
        }

        private bool CanDisplay(int displayNum, int totalNum)
        {
            return totalNum > displayNum;
        }

        private int DistanceToIndex(float distance, int displayNum, int totalNum, int totalLength)
        {
            return (int)(distance / totalLength * (totalNum - displayNum) * totalNum / (totalNum - displayNum));
        }

        /// <summary>
        /// Converts the index to the vertical distance from the topmost 
        /// position in view to current position in the view. Top boundary 
        /// reaches the top of the ViewRegion. Bottom boundary reaches the 
        /// bottom of the ViewRegion.
        /// </summary>
        private float IndexToDistance(int offsetNum, int displayNum, int totalNum, int totalLength)
        {
            return totalLength * (float)offsetNum / (totalNum - displayNum) * (1 - (float)displayNum / totalNum);
        }

        private Rectangle IndexToRectangle()
        {
            var distance = this.IndexToDistance(
                this.viewScroll.RowOffset,
                this.viewSettings.ViewRowDisplay,
                this.viewLayout.RowNum,
                this.viewRegion.Height);

            var location = new Point(
                this.viewRegion.X + this.viewRegion.Width,
                this.viewRegion.Y + (int)Math.Ceiling(distance));

            var size = new Point(
                this.ScrollbarSettings.Width,
                this.viewRegion.Height * this.viewSettings.ViewRowDisplay / this.viewLayout.RowNum);

            return new Rectangle(location, size);
        }

        #endregion

        #region Draw

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            base.Draw(graphics, time, alpha);

            if (this.CanDisplay())
            {
                this.scrollbarVisual.Draw(graphics, time, alpha);
            }
        }

        public void Toggle()
        {
            this.scrollbarVisual.Toggle();
        }

        #endregion

        #region Update

        public override void Update(GameTime time)
        {
            base.Update(time);

            this.UpdatePosition(time);
            this.UpdateVisual(time);
        }

        private void UpdateVisual(GameTime time)
        {
            if (this.StateMachine.IsInState(State.Dragging))
            {
                this.scrollbarVisual.Toggle();
            }

            this.scrollbarVisual.Update(time);
        }

        private void UpdatePosition(GameTime time)
        {
            if (this.CanDisplay())
            {
                if (!this.StateMachine.IsInState(State.Dragging))
                {
                    this.Rectangle = this.IndexToRectangle();
                }
                else
                {
                    this.Rectangle = new Rectangle(new Point(this.viewRegion.X + this.viewRegion.Width, this.Rectangle.Y), this.Rectangle.Size);
                    this.viewScroll.RowOffset = this.DistanceToIndex(this.Rectangle.Y - this.viewRegion.Y, this.viewSettings.ViewRowDisplay, this.viewLayout.RowNum, this.viewRegion.Height);
                }
            }
        }

        #endregion
    }
}