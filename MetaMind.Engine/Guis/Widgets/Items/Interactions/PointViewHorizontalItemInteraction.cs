namespace MetaMind.Engine.Guis.Widgets.Items.Interactions
{
    using MetaMind.Engine.Guis.Widgets.Items.Layers;
    using MetaMind.Engine.Guis.Widgets.Items.Layouts;
    using MetaMind.Engine.Guis.Widgets.Views.Layers;
    using MetaMind.Engine.Guis.Widgets.Views.Selections;
    using MetaMind.Engine.Guis.Widgets.Views.Swaps;
    using MetaMind.Engine.Services;

    public class PointViewHorizontalItemInteraction : ViewItemInteraction, IViewItemViewSelectionProvider, IViewItemViewSwapProvider
    {
        private IPointViewHorizontalSelectionController viewSelection;

        private IViewSwapController viewSwap;


        public PointViewHorizontalItemInteraction(IViewItem item, IViewItemLayout itemLayout, IViewItemLayoutInteraction itemLayoutInteraction)
            : base(item, itemLayout, itemLayoutInteraction)
        {
        }

        public override void SetupLayer()
        {
            var itemLayer = this.ItemGetLayer<PointViewHorizontalItemLayer>();
            var viewLayer = this.ViewGetLayer<PointViewHorizontalLayer>();

            this.viewSelection = viewLayer.ViewSelection;
            this.viewSwap = viewLayer.ViewSwap;
        }

        public void ViewDoSelect()
        {
            this.ItemLayoutInteraction.ViewDoSelect(this.ItemLayout);
        }

        public void ViewDoUnselect()
        {
            this.ItemLayoutInteraction.ViewDoUnselect(this.ItemLayout);
        }

        public void ViewUpdateSelection()
        {
            if (this.viewSelection.IsSelected(this.ItemLayout.Id))
            {
                // Unify mouse and keyboard selection
                if (!this.Item[ItemState.Item_Is_Selected]())
                {
                    this.ItemSelect();
                }

                this.Item[ItemState.Item_Is_Selected] = () => true;
            }
            else
            {
                // Unify mouse and keyboard selection
                if (this.Item[ItemState.Item_Is_Selected]())
                {
                    this.ItemSelect();
                }

                this.Item[ItemState.Item_Is_Selected] = () => false;
            }
            
        }

        public void ViewDoSwap(IGameInteropService interop, IViewItem draggingItem)
        {
            if (this.Item[ItemState.Item_Is_Swaping]())
            {
                return;
            }

            this.viewSwap.StartProcess(interop, this.Item, draggingItem, draggingItem.View);
        }

        public void ViewUpdateSwap()
        {
            if (this.Item[ItemState.Item_Is_Dragging]())
            {
                this.viewSwap.WatchProcess(this.Item);
            }
        }
    }
}