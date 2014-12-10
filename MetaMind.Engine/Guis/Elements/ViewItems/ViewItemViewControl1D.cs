// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewItemViewControl1D.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Elements.ViewItems
{
    using System;

    using MetaMind.Engine.Guis.Elements.Items;
    using MetaMind.Engine.Guis.Elements.Views;
    using Microsoft.Xna.Framework;

    public class ViewItemViewControl1D : ViewItemComponent
    {
        public ViewItemViewControl1D(IViewItem item)
            : base(item)
        {
        }

        public void MouseSelectIt()
        {
            ViewControl.Selection.Select(ItemControl.Id);
        }

        public void MouseUnselectIt()
        {
            if (ViewControl.Selection.IsSelected(ItemControl.Id))
            {
                ViewControl.Selection.Clear();
            }
        }

        public virtual void ExchangeIt(IViewItem draggingItem, IView targetView)
        {
            if (Item.IsEnabled(ItemState.Item_Exchanging))
            {
                return;
            }

            Item.Enable(ItemState.Item_Exchanging);

            ProcessManager.AttachProcess(new ViewItemExchangeProcess(draggingItem, targetView));
        }

        public virtual void SwapIt(IViewItem draggingItem)
        {
            if (Item.IsEnabled(ItemState.Item_Swaping))
            {
                return;
            }

            Item.Enable(ItemState.Item_Swaping);

            var originCenter = this        .ViewControl.Scroll.RootCenterPoint(this        .ItemControl.Id);
            var targetCenter = draggingItem.ViewControl.Scroll.RootCenterPoint(draggingItem.ItemControl.Id);

            ViewControl.Swap.Initialize(originCenter, targetCenter);

            ProcessManager.AttachProcess(new ViewItemSwapProcess(draggingItem, Item));
        }

        public virtual void UpdateStructure(GameTime gameTime)
        {
            this.UpdateViewScroll();
            this.UpdateViewSelection();

            // to improve performance
            if (ItemControl.Active)
            {
                this.UpdateViewSwap();
            }
        }

        protected virtual void UpdateViewScroll()
        {
            ItemControl.Id = View.Items.IndexOf(Item);

            if (View.IsEnabled(ViewState.View_Active) &&
                ViewControl.Scroll.CanDisplay(ItemControl.Id))
            {
                Item.Enable(ItemState.Item_Active);
            }
            else
            {
                Item.Disable(ItemState.Item_Active);
            }
        }

        /// <summary>
        /// Item active state determination based on selection and view state
        /// </summary>
        private void UpdateViewSelection()
        {
            if (ViewControl.Selection.IsSelected(ItemControl.Id))
            {
                // unify mouse and keyboard selection
                if (!Item.IsEnabled(ItemState.Item_Selected))
                {
                    ItemControl.CommonSelectIt();
                }

                Item.Enable(ItemState.Item_Selected);
            }
            else
            {
                // unify mouse and keyboard selection
                if (Item.IsEnabled(ItemState.Item_Selected))
                {
                    ItemControl.CommonUnselectIt();
                }

                Item.Disable(ItemState.Item_Selected);
            }
        }

        private void UpdateViewSwap()
        {
            if (Item.IsEnabled(ItemState.Item_Dragging))
            {
                foreach (var observor in ViewControl.Swap.Observors)
                {
                    ViewControl.Swap.WatchSwapFrom(Item, observor);
                }

                Predicate<IView> another = view => !ReferenceEquals(view, View);
                foreach (var observor in ViewControl.Swap.Observors.FindAll(another))
                {
                    ViewControl.Swap.WatchExchangeIn(Item, observor);
                }
            }
        }
    }
}