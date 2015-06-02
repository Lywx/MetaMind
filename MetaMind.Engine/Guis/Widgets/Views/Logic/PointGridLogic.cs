namespace MetaMind.Engine.Guis.Widgets.Views.Logic
{
    using System.Collections.Generic;

    using Items.Factories;
    using Layers;
    using Layouts;
    using Scrolls;
    using Selections;
    using Services;
    using Swaps;
    using Visuals;

    using Microsoft.Xna.Framework;

    public class PointGridLogic<TData> : PointView2DLogic<TData>, IPointGridLogic
    {
        private IPointView2DSelectionController viewSelection;

        private IPointView2DScrollController viewScroll;

        public PointGridLogic(
            IView                    view,
            IList<TData>             viewData,
            IViewScrollController    viewScroll,
            IViewSelectionController viewSelection,
            IViewSwapController      viewSwap,
            IViewLayout              viewLayout,
            IViewItemFactory itemFactory)
            : base(view, viewData, viewScroll, viewSelection, viewSwap, viewLayout, itemFactory)
        {
            this.Region = new ViewRegion(this.RegionBounds);
            this.Scrollbar = new ViewVerticalScrollBar(viewSettings, viewLayer.ViewScroll, this, this.Region, viewSettings.ScrollbarSettings);

            this.View[ViewState.View_Has_Focus] = () => this.Region[RegionState.Region_Has_Focus]() || this.View[ViewState.View_Has_Selection]();
        }

        public override void SetupLayer()
        {
            var viewLayer = this.ViewGetLayer<PointView2DLayer>();
            this.viewSelection = viewLayer.ViewSelection;
            this.viewScroll = viewLayer.ViewScroll;
        }

        #region Operations

        public override void MoveDown()
        {
            this.ViewGetComponent<ViewVerticalScrollBar>("verticalScrollbar").Trigger();
            this.viewSelection.MoveDown();
        }

        public override void MoveLeft()
        {
            this.ViewGetComponent<ViewVerticalScrollBar>("verticalScrollbar").Trigger();
            this.viewSelection.MoveLeft();
        }

        public override void MoveRight()
        {
            this.ViewGetComponent<ViewVerticalScrollBar>("verticalScrollbar").Trigger();
            this.viewSelection.MoveRight();
        }

        public override void MoveUp()
        {
            this.ViewGetComponent<ViewVerticalScrollBar>("verticalScrollbar").Trigger();
            this.viewSelection.MoveUp();
        }

        public void ScrollDown()
        {
            this.ViewGetComponent<ViewVerticalScrollBar>("verticalScrollbar").Trigger();
            this.viewScroll.MoveDown();
        }

        public void ScrollUp()
        {
            this.ViewGetComponent<ViewVerticalScrollBar>("verticalScrollbar").Trigger();
            this.viewScroll.MoveUp();
        }

        #endregion Operations

        #region Update Structure

        public override void Update(GameTime time)
        {
            base          .Update(time);
            this.Region   .Update(time);
            this.Scrollbar.Update(time);
        }
        
        #endregion Update Structure

        #region Update Input

        public bool Locked
        {
            get { return this.View[ViewState.View_Is_Editing](); }
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.UpdateInputOfRegion(input, time);
            this.UpdateInputOfMouse(input, time);
            this.UpdateInputOfKeyboard(input, time);
            this.UpdateInputOfItems(input, time);
        }

        protected override void UpdateInputOfMouse(IGameInputService input, GameTime time)
        {
            if (this.View[ViewState.View_Is_Inputting]())
            {
                if (this.ViewSettings.MouseEnabled)
                {
                    if (input.State.Mouse.IsWheelScrolledUp)
                    {
                        this.ScrollUp();
                    }

                    if (input.State.Mouse.IsWheelScrolledDown)
                    {
                        this.ScrollDown();
                    }
                }
            }
        }

        protected void UpdateInputOfRegion(IGameInputService input, GameTime gameTime)
        {
            if (this.View[ViewState.View_Is_Inputting]())
            {
                this.Region.UpdateInput(input, gameTime);
            }
        }

        #endregion Update Input

        #region Configurations

        protected virtual Rectangle RegionBounds()
        {
            return new Rectangle(
                (int)this.ViewSettings.Position.X,
                (int)this.ViewSettings.Position.Y,
                this.ViewSettings.ColumnNumDisplay * this.ItemSettings.NameFrameSize.X,
                this.ViewSettings.RowNumDisplay    * this.ItemSettings.NameFrameSize.Y);
        }

        #endregion Configurations

    }
}