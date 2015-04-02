namespace MetaMind.Engine.Guis.Widgets.Items
{
    using System.Diagnostics;

    using MetaMind.Engine.Components.Processes;
    using MetaMind.Engine.Guis.Widgets.Views;

    using Microsoft.Xna.Framework;

    public class ViewItemSwapProcess : ProcessBase
    {
        private const int UpdateNum = 6;

        private bool initialized;

        public ViewItemSwapProcess(IViewItem draggingItem, IViewItem swappingItem, dynamic source = null)
        {
            this.DraggingItem = draggingItem;
            this.SwappingItem = swappingItem;
            this.SwapControl  = swappingItem.ViewControl.Swap;

            this.Source = source;

            this.initialized = true;
        }

        protected ViewItemSwapProcess()
        {
            this.initialized = false;
        }

        protected IViewItem DraggingItem { get; private set; }

        protected dynamic Source { get; private set; }

        protected IPointViewSwapControl SwapControl { get; private set; }

        protected IViewItem SwappingItem { get; private set; }

        public ViewItemSwapProcess Initalize(IViewItem draggingItem, IViewItem swappingItem, dynamic source = null)
        {
            if (this.initialized)
            {
                return this;
            }

            this.DraggingItem  = draggingItem;
            this.SwappingItem  = swappingItem;
            this.SwapControl   = swappingItem.ViewControl.Swap;

            this.Source = source;

            this.initialized = true;

            return this;
        }

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

            this.EndSwap();
        }

        public override void Update(GameTime gameTime)
        {
            this.SwapControl.Progress += 1f / UpdateNum;

            if (this.SwapControl.Progress > 1)
            {
                this.Succeed();
            }
        }

        protected void EndSwap()
        {
            // refine selection to make sure the overall effect is smooth
            this.DraggingItem.ItemControl.MouseSelectsIt();

            // stop swapping state
            this.SwappingItem.Disable(ItemState.Item_Swaping);
        }

        protected virtual void SwapAroundView()
        {
            var draggedExchangable  = this.DraggingItem as IViewItemExchangable;
            var swappingExchangable = this.SwappingItem as IViewItemExchangable;

            Debug.Assert(draggedExchangable != null && swappingExchangable != null, "Not all item are exchangeable.");

            // replace each another in their origial view
            var originalSwappingItemView = this.SwappingItem.View;
            var orignialDraggedItemView  = this.DraggingItem.View;

            orignialDraggedItemView.Control.Selection.Clear();
            orignialDraggedItemView.Disable(ViewState.View_Has_Focus);

            originalSwappingItemView.Control.Selection.Select(0);
            originalSwappingItemView.Enable(ViewState.View_Has_Focus);

            draggedExchangable.ExchangeTo(originalSwappingItemView, this.SwappingItem.ItemControl.Id);
            swappingExchangable.ExchangeTo(orignialDraggedItemView, this.DraggingItem.ItemControl.Id);
        }

        protected virtual void SwapDataInList()
        {
            if (this.Source == null || 
               !this.Source.Contains(this.DraggingItem.ItemData) || 
               !this.Source.Contains(this.SwappingItem.ItemData))
            {
                return;
            }

            int draggingPosition = this.Source.IndexOf(this.DraggingItem.ItemData);
            int swappingPosition = this.Source.IndexOf(this.SwappingItem.ItemData);

            this.Source[draggingPosition] = this.SwappingItem.ItemData;
            this.Source[swappingPosition] = this.DraggingItem.ItemData;
        }

        protected virtual void SwapInView()
        {
            // swap id then sort
            var swappingId = this.SwappingItem.ItemControl.Id;

            this.SwappingItem.ItemControl.Id = this.DraggingItem.ItemControl.Id;
            this.DraggingItem.ItemControl.Id = swappingId;

            this.SwappingItem.View.Control.SortItems(PointViewSortMode.Id);
        }
    }
}