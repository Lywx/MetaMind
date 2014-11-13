// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewItemViewControl1D.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Elements.ViewItems
{
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
            this.ViewControl.Selection.Select(this.ItemControl.Id);
        }

        public void MouseUnselectIt()
        {
            if (this.ViewControl.Selection.IsSelected(this.ItemControl.Id))
            {
                this.ViewControl.Selection.Clear();
            }
        }

        public virtual void SwapIt(IViewItem draggingItem)
        {
            if (this.Item.IsEnabled(ItemState.Item_Swaping))
            {
                return;
            }

            this.Item.Enable(ItemState.Item_Swaping);

            var originCenter = this.ViewControl.Scroll.RootCenterPoint(this.ItemControl.Id);
            var targetCenter = draggingItem.ViewControl.Scroll.RootCenterPoint(draggingItem.ItemControl.Id);

            this.ViewControl.Swap.Initialize(originCenter, targetCenter);

            ProcessManager.AttachProcess(new ViewItemSwapProcess(draggingItem, this.Item));
        }

        public virtual void UpdateStructure(GameTime gameTime)
        {
            this.UpdateViewScroll();

            // to improve performance
            if (ItemControl.Active)
            {
                this.UpdateViewSelection();
                this.UpdateViewSwap();
            }
        }

        protected virtual void UpdateViewScroll()
        {
            this.ItemControl.Id = this.View.Items.IndexOf(this.Item);

            if (View.IsEnabled(ViewState.View_Active) &&
                ViewControl.Scroll.CanDisplay(ItemControl.Id))
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
                // not sure whether fired only once
                if (!this.Item.IsEnabled(ItemState.Item_Selected))
                {
                    this.ItemControl.CommonSelectIt();
                }

                this.Item.Enable(ItemState.Item_Selected);
            }
            else
            {
                // unify mouse and keyboard selection
                // not sure whether fired only once
                if (this.Item.IsEnabled(ItemState.Item_Selected))
                {
                    this.ItemControl.CommonUnselectIt();
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
                    this.ViewControl.Swap.ObserveSwapFrom(this.Item, observor);
                }
            }
        }
    }
}