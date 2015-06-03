namespace MetaMind.Engine.Guis.Widgets.Views.Swaps
{
    using System.Collections.Generic;
    using Items;
    using Items.Layers;
    using Items.Swaps;
    using Services;

    public class BlockViewVerticalSwapController<TData> : ViewSwapController<TData> 
    {
        public BlockViewVerticalSwapController(IView view, IList<TData> viewData)
            : base(view, viewData)
        {
        }
        
        public override void StartProcess(IGameInteropService interop, IViewItem touchedItem, IViewItem draggingItem, IView draggingView)
        {
            this.HasStarted = true;
            this.Progress   = 0f;

            // Set swapping state 
            touchedItem[ItemState.Item_Is_Swaping] = () => true;

            // Set start point
            var touchedItemLayer = touchedItem.ItemLogic.ItemGetLayer<BlockViewVerticalItemLayer>();
            this.Start = this.View.ViewLogic.ViewScroll.Position(touchedItemLayer.ItemLayout.Row);

            // Set end point
            var draggingItemLayer = draggingItem.ItemLogic.ItemGetLayer<BlockViewVerticalItemLayer>();
            this.End = draggingView.ViewLogic.ViewScroll.Position(draggingItemLayer.ItemLayout.Row);

            interop.Process.AttachProcess(new ViewItemSwapProcess<TData>(
                draggingItem,
                draggingItem.ItemLogic,
                draggingView.ViewLogic,
                touchedItem,
                touchedItem.ItemLogic,
                this.View.ViewLogic,
                this.ViewData));
        }

    }
}