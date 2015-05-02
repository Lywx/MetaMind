// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewItemViewControl.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Items.Views
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Items.Logic;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public abstract class ViewItemViewControl : ViewItemComponent, IViewItemViewControl
    {
        public ViewItemViewControl(IViewItem item)
            : base(item)
        {
            this.PassInViewLogic();
        }

        #region View State Logic

        public abstract Func<bool> ItemIsActive { get; }

        public void PassInViewLogic()
        {
            this.Item[ItemState.Item_Is_Active] = this.ItemIsActive;
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

        public void ViewDoSwap(IGameInteropService interop, IViewItem draggingItem)
        {
            var provider = this as IViewItemViewSwapProvider;
            if (provider != null)
            {
                provider.ViewSwap(interop, draggingItem);
            }
        }

        public void ViewDoTransit(IGameInteropService interop, IViewItem srcItem, IView desView)
        {
            var provider = this as IViewItemViewTransitProvider;
            if (provider != null)
            {
                provider.ViewTransit(interop, srcItem, desView);
            }
        }

        #endregion

        #region Update

        public override void Update(GameTime time)
        {
            this.UpdateViewItem();
            this.UpdateViewSelection();

            // For better performance
            if (this.Item[ItemState.Item_Is_Inputting]())
            {
                this.UpdateViewSwap();
            }
        }

        protected virtual void UpdateViewItem()
        {
            this.ItemExtension.Get<ViewItemLogic>().Id = this.View.ViewItems.IndexOf(this.Item);
        }

        /// <summary>
        /// Item active state determination based on selection and view state
        /// </summary>
        private void UpdateViewSelection()
        {
            var provider = this as IViewItemViewSelectionProvider;
            if (provider != null)
            {
                provider.ViewSelectionUpdate();
            }
        }

        private void UpdateViewSwap()
        {
            if (this.Item[ItemState.Item_Is_Dragging]())
            {
                foreach (var observor in this.ViewLogic.ViewSwap.Observors)
                {
                    this.ViewLogic.Swap.WatchSwapFrom(this.Item, observor);
                }

                Predicate<IView> another = view => !ReferenceEquals(view, this.View);
                foreach (var observor in this.ViewLogic.ViewSwap.Observors.FindAll(another))
                {
                    this.ViewLogic.Swap.WatchExchangeIn(this.Item, observor);
                }
            }
        }

        #endregion
    }
}