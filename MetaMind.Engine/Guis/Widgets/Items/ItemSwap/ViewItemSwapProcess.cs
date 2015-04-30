namespace MetaMind.Engine.Guis.Widgets.Items
{
    using System.Diagnostics;

    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Guis.Widgets.Views.PointView;
    using MetaMind.Engine.Guis.Widgets.Views.PointView.ViewSwap;

    using Microsoft.Xna.Framework;

    using Process = MetaMind.Engine.Components.Processes.Process;

    ///  TODO: Variable name need to be changed

    public class ViewItemSwapProcess : Process
    {
        private const int UpdateNum = 6;

        private bool initialized;

        protected dynamic CommonSource { get; private set; }

        protected IViewItem DraggingItem { get; private set; }

        protected IViewSwapControl Control { get; private set; }

        protected IViewItem SwappingItem { get; private set; }

        #region Constructors

        public ViewItemSwapProcess(IViewItem draggingItem, IViewItem swappingItem, dynamic commonSource = null)
        {
            this.DraggingItem = draggingItem;

            this.SwappingItem    = swappingItem;
            this.Control = swappingItem.ViewLogic.Swap;

            this.CommonSource = commonSource;

            this.initialized = true;
        }

        protected ViewItemSwapProcess()
        {
            this.initialized = false;
        }

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
            this.Control = swappingItem.ViewLogic.Swap;

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
            this.Control.Progress += 1f / UpdateNum;

            if (this.Control.Progress > 1)
            {
                this.Succeed();
            }
        }

        #endregion

        #region ViewSwap Operations

        protected virtual void SwapAroundView()
        {
            var draggedExchangable = this.DraggingItem as IViewItemExchangable;
            var swappingExchangable = this.SwappingItem as IViewItemExchangable;

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