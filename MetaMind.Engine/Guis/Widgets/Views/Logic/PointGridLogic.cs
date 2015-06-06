namespace MetaMind.Engine.Guis.Widgets.Views.Logic
{
    using System.Collections.Generic;

    using Microsoft.Xna.Framework;

    using Items.Factories;
    using Layouts;
    using Scrolls;
    using Selections;
    using Services;
    using Swaps;
    using Visuals;
    using Regions;

    public class PointGridLogic<TData> : PointView2DLogic<TData>, IPointGridLogic
    {
        private ViewVerticalScrollBar viewVerticalScrollbar;

        private ViewRegion viewRegion;

        public PointGridLogic(
            IView view,
            IList<TData> viewData,
            IViewScrollController viewScroll,
            IViewSelectionController viewSelection,
            IViewSwapController viewSwap,
            IViewLayout viewLayout,
            IViewItemFactory itemFactory)
            : base(view, viewData, viewScroll, viewSelection, viewSwap, viewLayout, itemFactory)
        {
        }

        #region Configurations

        #endregion Configurations

        #region Layer

        public override void SetupLayer()
        {
            this.viewRegion = new ViewRegion(() => new Rectangle(
                this.ViewSettings.ViewPosition.ToPoint(),
                new Point(
                    (int)(this.ViewSettings.ViewColumnDisplay * this.ViewSettings.ItemMargin.X),
                    (int)(this.ViewSettings.ViewRowDisplay    * this.ViewSettings.ItemMargin.Y))));

            this.ViewComponents.Add("ViewRegion", this.viewRegion);

            this.View[ViewState.View_Has_Focus] = () => this.viewRegion[RegionState.Region_Has_Focus]() || this.View[ViewState.View_Has_Selection]();

            var viewVerticalScrollbarSettings = this.ViewSettings.Get<ViewScrollbarSettings>("ViewVericalScrollbar");
            this.viewVerticalScrollbar = new ViewVerticalScrollBar(this.ViewSettings, this.ViewScroll, this.ViewLayout, this.viewRegion, viewVerticalScrollbarSettings);
            this.ViewComponents.Add("ViewVerticalScrollbar", this.viewVerticalScrollbar);
        }

        #endregion

        #region Operations

        public override void MoveDown()
        {
            this.viewVerticalScrollbar.Trigger();
            this.ViewSelection.MoveDown();
        }

        public override void MoveLeft()
        {
            this.viewVerticalScrollbar.Trigger();
            this.ViewSelection.MoveLeft();
        }

        public override void MoveRight()
        {
            this.viewVerticalScrollbar.Trigger();
            this.ViewSelection.MoveRight();
        }

        public override void MoveUp()
        {
            this.viewVerticalScrollbar.Trigger();
            this.ViewSelection.MoveUp();
        }

        public override void ScrollDown()
        {
            this.viewVerticalScrollbar.Trigger();
            this.ViewScroll.MoveDown();
        }

        public override void ScrollUp()
        {
            this.viewVerticalScrollbar.Trigger();
            this.ViewScroll.MoveUp();
        }

        #endregion Operations

        #region Update Structure

        public override void Update(GameTime time)
        {
            base.Update(time);
            this.viewRegion.Update(time);
            this.viewVerticalScrollbar.Update(time);
        }

        #endregion Update Structure

        #region Update Input

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.UpdateInputOfRegion(input, time);
            this.UpdateInputOfMouse(input, time);
            this.UpdateInputOfKeyboard(input, time);
            this.UpdateInputOfItems(input, time);
        }

        protected void UpdateInputOfRegion(IGameInputService input, GameTime gameTime)
        {
            if (this.View[ViewState.View_Is_Inputting]())
            {
                this.viewRegion.UpdateInput(input, gameTime);
            }
        }

        #endregion Update Input
    }
}