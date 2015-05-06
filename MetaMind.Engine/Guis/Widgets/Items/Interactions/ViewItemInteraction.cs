// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewItemInteraction.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Items.Interactions
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Items.Layouts;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class ViewItemInteraction : ViewItemComponent, IViewItemInteraction
    {
        public ViewItemInteraction(IViewItem item, IViewItemLayout itemLayout, IViewItemLayoutInteraction itemLayoutInteraction)
            : base(item)
        {
            if (itemLayout == null)
            {
                throw new ArgumentNullException("itemLayout");
            }

            if (itemLayoutInteraction == null)
            {
                throw new ArgumentNullException("itemLayoutInteraction");
            }

            this.ItemLayout            = itemLayout;
            this.ItemLayoutInteraction = itemLayoutInteraction;
        }

        public IViewItemLayoutInteraction ItemLayoutInteraction { get; set; }

        public IViewItemLayout ItemLayout { get; set; }

        #region Update

        public override void Update(GameTime time)
        {
            this.UpdateViewSelection();

            // For better performance
            if (this.Item[ItemState.Item_Is_Inputting]())
            {
                this.UpdateViewSwap();
            }
        }

        /// <summary>
        ///     Item active state determination based on selection and view state
        /// </summary>
        private void UpdateViewSelection()
        {
            var provider = this as IViewItemViewSelectionProvider;
            if (provider != null)
            {
                provider.ViewUpdateSelection();
            }
        }

        private void UpdateViewSwap()
        {
            var provider = this as IViewItemViewSwapProvider;
            if (provider != null)
            {
                provider.ViewUpdateSwap();
            }
        }

        #endregion

        #region View Interaction Providers

        public void ViewSelect()
        {
            var provider = this as IViewItemViewSelectionProvider;
            if (provider != null)
            {
                provider.ViewDoSelect();
            }
        }

        public void ViewUnselect()
        {
            var provider = this as IViewItemViewSelectionProvider;
            if (provider != null)
            {
                provider.ViewDoUnselect();
            }
        }

        public void ViewSwap(IGameInteropService interop, IViewItem draggingItem)
        {
            var provider = this as IViewItemViewSwapProvider;
            if (provider != null)
            {
                provider.ViewDoSwap(interop, draggingItem);
            }
        }

        #endregion

        #region Item Interaction

        public virtual void ItemSelect()
        {
        }

        #endregion
    }
}