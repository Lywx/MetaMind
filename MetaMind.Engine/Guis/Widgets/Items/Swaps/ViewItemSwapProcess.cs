namespace MetaMind.Engine.Guis.Widgets.Items.Swaps
{
    using System;
    using System.Diagnostics;

    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Guis.Widgets.Views.PointView;
    using MetaMind.Engine.Guis.Widgets.Views.Swaps;

    using Microsoft.Xna.Framework;

    using Process = MetaMind.Engine.Components.Processes.Process;

    public class ViewItemSwapProcess : Process
    {
        private const int UpdateNum = 6;

        private bool initialized; // TODO:FIXME

        #region Constructors

        public ViewItemSwapProcess(IViewItem draggingItem, IViewItem swappingItem, dynamic commonSource = null)
        {
            if (draggingItem == null)
            {
                throw new ArgumentNullException("draggingItem");
            }

            if (swappingItem == null)
            {
                throw new ArgumentNullException("swappingItem");
            }

            this.DraggingItem = draggingItem;
            this.SwappingItem = swappingItem;

            this.SwapControl = swappingItem.ViewLogic.ViewSwap;

            this.CommonSource = commonSource;

            this.initialized = true;
        }

        protected ViewItemSwapProcess()
        {
            this.initialized = false;
        }

        #endregion

        #region Dependency

        protected IViewItem DraggingItem { get; private set; }

        protected IViewItem SwappingItem { get; private set; }

        protected IViewSwapControl SwapControl { get; private set; }

        #endregion

        #region Initializations

        public ViewItemSwapProcess Initialize(IViewItem draggingItem, IViewItem swappingItem, dynamic commonSource = null)
        {
            if (this.initialized)
            {
                return this;
            }

            this.DraggingItem = draggingItem;
            this.SwappingItem = swappingItem;

            this.SwapControl = swappingItem.ViewLogic.Swap;

            this.CommonSource = commonSource;

            this.initialized = true;

            return this;
        }

        #endregion

        #region ViewSwap Transition

        public override void OnAbort()
        {
        }

        public override void OnFail()
        {
        }

        public override void OnSuccess()
        {
            var inSameView = ReferenceEquals(this.SwappingItem.View, this.DraggingItem.View);
            if (inSameView)
            {
                this.SwapInView();
            }
            else
            {
                this.SwapAroundView();
            }

            this.SwapTerminate();
        }


        #endregion

        #region ViewSwap Update

        public override void Update(GameTime time)
        {
            this.SwapControl.Progress += 1f / UpdateNum;

            if (this.SwapControl.Progress > 1)
            {
                this.Succeed();
            }
        }

        #endregion

        #region ViewSwap Operations

        protected virtual void SwapAroundView()
        {
            var draggedExchangable = this.DraggingItem;
            var swappingExchangable = this.SwappingItem;

            // assert all exchangable
            {
                Debug.Assert(
                    draggedExchangable != null && swappingExchangable != null,
                    "Not all item are exchangeable.");
            }

            // replace each another in their origial view
            var originalSwappingItemView = this.SwappingItem.View;
            var orignialDraggedItemView = this.DraggingItem.View;

            orignialDraggedItemView.ViewLogic.Selection.Clear();
            orignialDraggedItemView[ViewState.View_Has_Focus] = () => false;

            originalSwappingItemView.ViewLogic.Selection.Select(0);
            originalSwappingItemView[ViewState.View_Has_Focus] = () => true;

            draggedExchangable.ExchangeTo(originalSwappingItemView, this.SwappingItem.ItemLogic.Id);
            swappingExchangable.ExchangeTo(orignialDraggedItemView, this.DraggingItem.ItemLogic.Id);
        }

        protected virtual void SwapDataInList()
        {
            if (this.CommonSource == null || !this.CommonSource.Contains(this.DraggingItem.ItemData)
                || !this.CommonSource.Contains(this.SwappingItem.ItemData))
            {
                return;
            }

            int draggingPosition = this.CommonSource.IndexOf(this.DraggingItem.ItemData);
            int swappingPosition = this.CommonSource.IndexOf(this.SwappingItem.ItemData);

            this.CommonSource[draggingPosition] = this.SwappingItem.ItemData;
            this.CommonSource[swappingPosition] = this.DraggingItem.ItemData;
        }

        protected virtual void SwapInView()
        {
            // swap id then sort
            var swappingId = this.SwappingItem.ItemLogic.Id;

            this.SwappingItem.ItemLogic.Id = this.DraggingItem.ItemLogic.Id;
            this.DraggingItem.ItemLogic.Id = swappingId;

            this.SwappingItem.View.ViewLogic.SortItems(PointViewSortMode.Id);
        }

        protected void SwapTerminate()
        {
            // refine selection to make sure the overall effect is smooth
            this.DraggingItem.ItemLogic.MouseSelectsIt();

            // stop swapping state
            this.SwappingItem[ItemState.Item_Is_Swaping]();
        }
        #endregion
    }
}