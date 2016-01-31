namespace MetaMind.Engine.Core.Entity.Control.Item.Interactions
{
    using System;
    using Layouts;
    using Microsoft.Xna.Framework;
    using Services;

    public class MMViewItemInteraction : MMViewItemControllerComponent, IMMViewItemInteraction
    {
        public MMViewItemInteraction(IMMViewItem item, IMMViewItemLayout itemLayout, IViewItemLayoutInteraction itemLayoutInteraction)
            : base(item)
        {
            if (itemLayout == null)
            {
                throw new ArgumentNullException(nameof(itemLayout));
            }

            if (itemLayoutInteraction == null)
            {
                throw new ArgumentNullException(nameof(itemLayoutInteraction));
            }

            this.ItemLayout            = itemLayout;
            this.ItemLayoutInteraction = itemLayoutInteraction;
        }

        public IViewItemLayoutInteraction ItemLayoutInteraction { get; set; }

        public IMMViewItemLayout ItemLayout { get; set; }

        #region Update

        public override void Update(GameTime time)
        {
            base.Update(time);

            this.UpdateViewScroll(time);
            this.UpdateViewSelection(time);

            // For better performance
            if (this.Item[MMViewItemState.Item_Is_Inputing]())
            {
                this.UpdateViewSwap(time);
            }
        }

        private void UpdateViewScroll(GameTime time)
        {
            this.ItemLayout.Update(time);
        }

        /// <summary>
        ///     Item active state determination based on selection and view state
        /// </summary>
        /// <param name="time"></param>
        private void UpdateViewSelection(GameTime time)
        {
            var provider = this as IViewItemViewSelectionProvider;
            provider?.ViewUpdateSelection(time);
        }

        private void UpdateViewSwap(GameTime time)
        {
            var provider = this as IViewItemViewSwapProvider;
            provider?.ViewUpdateSwap();
        }

        #endregion

        #region View Interaction Providers

        public void ViewSelect()
        {
            var provider = this as IViewItemViewSelectionProvider;
            provider?.ViewDoSelect();
        }

        public void ViewUnselect()
        {
            var provider = this as IViewItemViewSelectionProvider;
            provider?.ViewDoUnselect();
        }

        public void ViewSwap(IMMEngineInteropService interop, IMMViewItem draggingItem)
        {
            var provider = this as IViewItemViewSwapProvider;
            provider?.ViewDoSwap(interop, draggingItem);
        }

        #endregion

        #region Item Interaction

        public void ItemSelect()
        {
            ((MMViewItem)this.Item).OnSelected();
        }

        public void ItemUnselect()
        {
            ((MMViewItem)this.Item).OnUnselected();
        }

        #endregion
    }
}