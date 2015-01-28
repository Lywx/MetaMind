// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewItemViewControl1D.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Items
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Views;

    using Microsoft.Xna.Framework;

    public class ViewItemViewControl1D : ViewItemComponent
    {
        public ViewItemViewControl1D(IViewItem item)
            : base(item)
        {
        }

        public void MouseSelectsIt()
        {
            this.ViewControl.Selection.Select(this.ItemControl.Id);
        }

        public void MouseUnselectsIt()
        {
            if (this.ViewControl.Selection.IsSelected(this.ItemControl.Id))
            {
                this.ViewControl.Selection.Clear();
            }
        }

        public virtual void ExchangeIt(IViewItem draggingItem, IView targetView)
        {
            if (this.Item.IsEnabled(ItemState.Item_Exchanging))
            {
                return;
            }

            this.Item.Enable(ItemState.Item_Exchanging);

            ProcessManager.AttachProcess(new ViewItemExchangeProcess(draggingItem, targetView));
        }

        public virtual void SwapIt(IViewItem draggingItem)
        {
            if (this.Item.IsEnabled(ItemState.Item_Swaping))
            {
                return;
            }

            this.Item.Enable(ItemState.Item_Swaping);

            var originCenter = this        .ViewControl.Scroll.RootCenterPoint(this        .ItemControl.Id);
            var targetCenter = draggingItem.ViewControl.Scroll.RootCenterPoint(draggingItem.ItemControl.Id);

            this.ViewControl.Swap.Initialize(originCenter, targetCenter);

            ProcessManager.AttachProcess(new ViewItemSwapProcess(draggingItem, this.Item));
        }

        public virtual void UpdateStructure(GameTime gameTime)
        {
            this.UpdateViewScroll();
            this.UpdateViewSelection();

            // to improve performance
            if (this.ItemControl.Active)
            {
                this.UpdateViewSwap();
            }
        }

        protected virtual void UpdateViewScroll()
        {
            this.ItemControl.Id = this.View.Items.IndexOf(this.Item);

            if (this.View.IsEnabled(ViewState.View_Active) &&
                this.ViewControl.Scroll.CanDisplay(this.ItemControl.Id))
            {
                this.Item.Enable(ItemState.Item_Active);
            }
            else
            {
                this.Item.Disable(ItemState.Item_Active);
            }
        }

        /// <summary>
        /// Item active state determination based on selection and view state
        /// </summary>
        private void UpdateViewSelection()
        {
            if (this.ViewControl.Selection.IsSelected(this.ItemControl.Id))
            {
                // unify mouse and keyboard selection
                if (!this.Item.IsEnabled(ItemState.Item_Selected))
                {
                    this.ItemControl.CommonSelectsIt();
                }

                this.Item.Enable(ItemState.Item_Selected);
            }
            else
            {
                // unify mouse and keyboard selection
                if (this.Item.IsEnabled(ItemState.Item_Selected))
                {
                    this.ItemControl.CommonUnselectsIt();
                }

                this.Item.Disable(ItemState.Item_Selected);
            }
        }

        private void UpdateViewSwap()
        {
            if (this.Item.IsEnabled(ItemState.Item_Dragging))
            {
                foreach (var observor in this.ViewControl.Swap.Observors)
                {
                    this.ViewControl.Swap.WatchSwapFrom(this.Item, observor);
                }

                Predicate<IView> another = view => !ReferenceEquals(view, this.View);
                foreach (var observor in this.ViewControl.Swap.Observors.FindAll(another))
                {
                    this.ViewControl.Swap.WatchExchangeIn(this.Item, observor);
                }
            }
        }
    }
}