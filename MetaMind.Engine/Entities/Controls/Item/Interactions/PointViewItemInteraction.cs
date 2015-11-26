namespace MetaMind.Engine.Entities.Controls.Item.Interactions
{
    using Layers;
    using Layouts;
    using Microsoft.Xna.Framework;
    using Services;
    using Views.Layers;
    using Views.Scrolls;
    using Views.Selections;
    using Views.Swaps;

    public class MMPointViewItemInteraction : MMViewItemInteraction, IViewItemViewSelectionProvider, IViewItemViewSwapProvider
    {
        private IMMViewSelectionController viewSelection;

        private IMMViewSwapController viewSwap;

        private IMMViewScrollController viewScroll;

        public MMPointViewItemInteraction(IMMViewItem item, IMMViewItemLayout itemLayout, IViewItemLayoutInteraction itemLayoutInteraction)
            : base(item, itemLayout, itemLayoutInteraction)
        {
        }

        #region Layer

        public override void Initialize()
        {
            var itemLayer = this.GetItemLayer<MMViewItemLayer>();
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

        public virtual void ViewDoSwap(IMMEngineInteropService interop, IMMViewItem draggingItem)
        {
            if (this.Item[MMViewItemState.Item_Is_Swaping]())
            {
                return;
            }

            var draggingItemLayer = draggingItem.GetLayer<MMPointViewItemLayer>();
            var draggingItemLayout = draggingItemLayer.ItemLayout;
            
            var draggingViewLayer = draggingItem.View.GetLayer<MMPointViewLayer>();
            var draggingViewScroll = draggingViewLayer.ViewScroll;

            this.viewSwap.StartProcess(this.Item,
                this.viewScroll.Position(this.ItemLayout.Id),
                draggingItem,
                draggingItem.View,
                draggingViewScroll.Position(draggingItemLayout.Id));
        }

        public void ViewUpdateSwap()
        {
            if (this.Item[MMViewItemState.Item_Is_Dragging]())
            {
                this.viewSwap.WatchProcess(this.Item);
            }
        }

        public void ViewUpdateSelection(GameTime time)
        {
            if (this.viewSelection.IsSelected(this.ItemLayout.Id))
            {
                // Unify mouse and keyboard selection
                if (!this.Item[MMViewItemState.Item_Is_Selected]())
                {
                    this.ItemSelect();
                }
            }
            else
            {
                // Unify mouse and keyboard selection
                if (this.Item[MMViewItemState.Item_Is_Selected]())
                {
                    this.ItemUnselect();
                }
            }
        }
    }
}