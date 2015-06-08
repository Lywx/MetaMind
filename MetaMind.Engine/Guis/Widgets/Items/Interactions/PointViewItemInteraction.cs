namespace MetaMind.Engine.Guis.Widgets.Items.Interactions
{
    using MetaMind.Engine.Guis.Widgets.Items.Layers;
    using MetaMind.Engine.Guis.Widgets.Items.Layouts;
    using MetaMind.Engine.Guis.Widgets.Views.Layers;
    using MetaMind.Engine.Guis.Widgets.Views.Selections;
    using MetaMind.Engine.Guis.Widgets.Views.Swaps;
    using MetaMind.Engine.Services;
    using Microsoft.Xna.Framework;
    using Views.Scrolls;

    public class PointViewItemInteraction : ViewItemInteraction, IViewItemViewSelectionProvider, IViewItemViewSwapProvider
    {
        private IViewSelectionController viewSelection;

        private IViewSwapController viewSwap;

        private IViewScrollController viewScroll;


        public PointViewItemInteraction(IViewItem item, IViewItemLayout itemLayout, IViewItemLayoutInteraction itemLayoutInteraction)
            : base(item, itemLayout, itemLayoutInteraction)
        {
        }

        public override void SetupLayer()
        {
            var itemLayer = this.ItemGetLayer<ViewItemLayer>();
            var viewLayer = this.ViewGetLayer<ViewLayer>();

            this.viewSelection = viewLayer.ViewSelection;
            this.viewSwap = viewLayer.ViewSwap;
            this.viewScroll = viewLayer.ViewScroll;
        }

        public void ViewDoSelect()
        {
            this.ItemLayoutInteraction.ViewDoSelect(this.ItemLayout);
        }

        public void ViewDoUnselect()
        {
            this.ItemLayoutInteraction.ViewDoUnselect(this.ItemLayout);
        }

        public void ViewUpdateSelection(GameTime time)
        {
            if (this.viewSelection.IsSelected(this.ItemLayout.Id))
            {
                // Unify mouse and keyboard selection
                if (!this.Item[ItemState.Item_Is_Selected]())
                {
                    this.ItemSelect();
                }
            }
            else
            {
                // Unify mouse and keyboard selection
                if (this.Item[ItemState.Item_Is_Selected]())
                {
                    this.ItemUnselect();
                }
            }
        }

        public virtual void ViewDoSwap(IGameInteropService interop, IViewItem draggingItem)
        {
            if (this.Item[ItemState.Item_Is_Swaping]())
            {
                return;
            }

            var draggingItemLayout = draggingItem.ItemLogic.ItemLayout;
            var draggingViewScroll = draggingItem.View.ViewLogic.ViewScroll;
            this.viewSwap.StartProcess(interop,
                this.Item,
                this.viewScroll.Position(this.ItemLayout.Id),
                draggingItem,
                draggingItem.View,
                draggingViewScroll.Position(draggingItemLayout.Id),
                );
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