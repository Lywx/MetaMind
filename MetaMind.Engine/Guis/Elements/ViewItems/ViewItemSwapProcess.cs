namespace MetaMind.Engine.Guis.Elements.ViewItems
{
    using System.Diagnostics;

    using MetaMind.Engine.Components.Processes;
    using MetaMind.Engine.Guis.Elements.Items;
    using MetaMind.Engine.Guis.Elements.Views;
    using Microsoft.Xna.Framework;

    public class ViewItemSwapProcess : ProcessBase
    {
        private const int UpdateNum = 6;

        public ViewItemSwapProcess(IViewItem draggedItem, IViewItem swappingItem)
        {
            this.DraggedItem  = draggedItem;
            this.SwappingItem = swappingItem;
            this.SwapControl  = swappingItem.ViewControl.Swap;
        }

        protected IViewItem DraggedItem { get; private set; }

        protected IViewSwapControl SwapControl { get; private set; }

        protected IViewItem SwappingItem { get; private set; }

        public override void OnAbort()
        {
        }

        public override void OnFail()
        {
        }

        public override void OnSuccess()
        {
            var inSameView = ReferenceEquals(this.SwappingItem.View, this.DraggedItem.View);
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
            this.DraggedItem.ItemControl.MouseSelectIt();

            // stop swapping state
            this.SwappingItem.Disable(ItemState.Item_Swaping);
        }

        protected virtual void SwapAroundView()
        {
            var draggedExchangable  = this.DraggedItem  as IViewItemExchangable;
            var swappingExchangable = this.SwappingItem as IViewItemExchangable;

            Debug.Assert(draggedExchangable != null && swappingExchangable != null, "Not all item are exchangeable.");

            // replace each another in their origial view
            var originalSwappingItemView = this.SwappingItem.View;
            var orignialDraggedItemView  = this.DraggedItem.View;

            orignialDraggedItemView.Control.Selection.Clear();
            orignialDraggedItemView.Disable(ViewState.View_Has_Focus);

            originalSwappingItemView.Control.Selection.Select(0);
            originalSwappingItemView.Enable(ViewState.View_Has_Focus);

            draggedExchangable .ExchangeTo(originalSwappingItemView, this.SwappingItem.ItemControl.Id);
            swappingExchangable.ExchangeTo(orignialDraggedItemView , this.DraggedItem.ItemControl.Id);
        }

        protected virtual void SwapInView()
        {
            // swap id then sort
            var swappingId = this.SwappingItem.ItemControl.Id;

            this.SwappingItem.ItemControl.Id = this.DraggedItem.ItemControl.Id;
            this.DraggedItem.ItemControl.Id = swappingId;

            this.SwappingItem.View.Control.SortItems(ViewSortMode.Id);
        }
    }
}