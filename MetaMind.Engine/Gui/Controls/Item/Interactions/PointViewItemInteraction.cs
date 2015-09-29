namespace MetaMind.Engine.Gui.Controls.Item.Interactions
{
    using Layers;
    using Layouts;
    using Microsoft.Xna.Framework;
    using Service;
    using Views.Layers;
    using Views.Scrolls;
    using Views.Selections;
    using Views.Swaps;

    public class PointViewItemInteraction : ViewItemInteraction, IViewItemViewSelectionProvider, IViewItemViewSwapProvider
    {
        private IViewSelectionController viewSelection;

        private IViewSwapController viewSwap;

        private IViewScrollController viewScroll;

        public PointViewItemInteraction(IViewItem item, IViewItemLayout itemLayout, IViewItemLayoutInteraction itemLayoutInteraction)
            : base(item, itemLayout, itemLayoutInteraction)
        {
        }

        #region Layer

        public override void Initialize()
        {
            var itemLayer = this.GetItemLayer<ViewItemLayer>();
            var viewLayer = this.GetViewLayer<ViewLayer>();

            this.viewSelection = viewLayer.ViewSelection;
            this.viewSwap = viewLayer.ViewSwap;
            this.viewScroll = viewLayer.ViewScroll;
        }

        #endregion

        public void ViewDoSelect()
        {
            this.ItemLayoutInteraction.ViewDoSelect(this.ItemLayout);
        }

        public void ViewDoUnselect()
        {
            this.ItemLayoutInteraction.ViewDoUnselect(this.ItemLayout);
        }

        public virtual void ViewDoSwap(IGameInteropService interop, IViewItem draggingItem)
        {
            if (this.Item[ViewItemState.Item_Is_Swaping]())
            {
                return;
            }

            var draggingItemLayer = draggingItem.GetLayer<PointViewItemLayer>();
            var draggingItemLayout = draggingItemLayer.ItemLayout;
            
            var draggingViewLayer = draggingItem.View.GetLayer<PointViewLayer>();
            var draggingViewScroll = draggingViewLayer.ViewScroll;

            this.viewSwap.StartProcess(interop,
                this.Item,
                this.viewScroll.Position(this.ItemLayout.Id),
                draggingItem,
                draggingItem.View,
                draggingViewScroll.Position(draggingItemLayout.Id));
        }

        public void ViewUpdateSwap()
        {
            if (this.Item[ViewItemState.Item_Is_Dragging]())
            {
                this.viewSwap.WatchProcess(this.Item);
            }
        }

        public void ViewUpdateSelection(GameTime time)
        {
            if (this.viewSelection.IsSelected(this.ItemLayout.Id))
            {
                // Unify mouse and keyboard selection
                if (!this.Item[ViewItemState.Item_Is_Selected]())
                {
                    this.ItemSelect();
                }
            }
            else
            {
                // Unify mouse and keyboard selection
                if (this.Item[ViewItemState.Item_Is_Selected]())
                {
                    this.ItemUnselect();
                }
            }
        }
    }
}