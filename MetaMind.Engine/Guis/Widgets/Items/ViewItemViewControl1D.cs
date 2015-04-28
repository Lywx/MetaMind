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
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class ViewItemViewControl1D : ViewItemComponent
    {
        public ViewItemViewControl1D(IViewItem item)
            : base(item)
        {
        }

        #region Selection

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

        #endregion

        #region Process

        public virtual void ExchangeIt(IGameInteropService interop, IViewItem draggingItem, IView targetView)
        {
            if (this.Item[ItemState.Item_Is_Transiting]())
            {
                return;
            }

            this.Item[ItemState.Item_Is_Transiting] = () => true;

            interop.Process.AttachProcess(new ViewItemTransitProcess(draggingItem, targetView));
        }

        public virtual void SwapIt(IGameInteropService interop, IViewItem draggingItem)
        {
            if (this.Item[ItemState.Item_Is_Swaping]())
            {
                return;
            }

            this.Item[ItemState.Item_Is_Swaping] = () => true;

            var originCenter = this.ViewControl.Scroll.RootCenterPoint(this.ItemControl.Id);
            var targetCenter = draggingItem.ViewLogic.Scroll.RootCenterPoint(draggingItem.ItemLogic.Id);

            this.ViewControl.Swap.Initialize(originCenter, targetCenter);

            interop.Process.AttachProcess(new ViewItemSwapProcess(draggingItem, this.Item));
        }


        #endregion

        #region Update

        public override void Update(GameTime time)
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

            // TODO: May be moved to ?
            this.Item[ItemState.Item_Is_Active] = () => this.View[ViewState.View_Is_Active]() && this.ViewControl.Scroll.CanDisplay(this.ItemControl.Id);
        }

        /// <summary>
        /// Item active state determination based on selection and view state
        /// </summary>
        private void UpdateViewSelection()
        {
            if (this.ViewControl.Selection.IsSelected(this.ItemControl.Id))
            {
                // Unify mouse and keyboard selection
                if (!this.Item[ItemState.Item_Is_Selected]())
                {
                    this.ItemControl.CommonSelectsIt();
                }

                this.Item[ItemState.Item_Is_Selected] = () => true;
            }
            else
            {
                // Unify mouse and keyboard selection
                if (this.Item[ItemState.Item_Is_Selected]())
                {
                    this.ItemControl.CommonUnselectsIt();
                }

                this.Item[ItemState.Item_Is_Selected] = () => false;
            }
        }

        private void UpdateViewSwap()
        {
            if (this.Item[ItemState.Item_Is_Dragging]())
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

        #endregion
    }
}