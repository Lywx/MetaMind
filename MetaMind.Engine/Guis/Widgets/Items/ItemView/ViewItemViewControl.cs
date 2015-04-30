// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewItemViewControl.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Items.ItemView
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Guis.Widgets.Views.PointView.ViewSelection;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class ViewItemViewControl : ViewItemComponent, IViewItemViewControl, IViewTransitContent, IViewSwapContent
    {
        public ViewItemViewControl(IViewItem item)
            : base(item)
        {
            this.Item[ItemState.Item_Is_Active] = () => this.View[ViewState.View_Is_Active]() && viewScroll.CanDisplay(this.ItemLogic.Id);
        }

        #region Dependency

        /// <summary>
        /// Gets the ItemLogic that would provide the corresponding service that this class would need.
        /// </summary>
        /// <remarks>
        /// Use of new keyword presumes this class is never used as ViewItemComponet(the super class).
        /// </remarks>
        protected virtual new IViewItemLogic ItemLogic
        {
            get
            {
                return base.ItemLogic;
            }
        }

        /// <summary>
        /// Gets the ItemLogic that would provide the corresponding service that this class would need.
        /// </summary>
        protected IViewSelectionControl ViewSelection
        {
            get
            {
                return ((IViewSelectionSupport)this.ViewLogic).ViewSelection;
            }
        }

        #endregion

        #region ViewSelection

        public void ViewDoSelect()
        {
            this.ViewSelection.Select(this.ItemLogic.Id);
        }

        public void ViewDoUnselect()
        {
            if (this.ViewSelection.IsSelected(this.ItemLogic.Id))
            {
                this.ViewSelection.Clear();
            }
        }

        #endregion

        #region Process

        public virtual void ViewDoSwap(IGameInteropService interop, IViewItem draggingItem)
        {
            if (this.Item[ItemState.Item_Is_Swaping]())
            {
                return;
            }

            this.Item[ItemState.Item_Is_Swaping] = () => true;

            var originCenter = this.ViewLogic.Scroll.RootCenterPoint(this.ItemLogic.Id);
            var targetCenter = draggingItem.ViewLogic.Scroll.RootCenterPoint(draggingItem.ItemLogic.Id);

            this.ViewLogic.Swap.Initialize(originCenter, targetCenter);

            interop.Process.AttachProcess(new ViewItemSwapProcess(draggingItem, this.Item));
        }

        public virtual void ViewDoTransit(IGameInteropService interop, IViewItem draggingItem, IView targetView)
        {
            if (this.Item[ItemState.Item_Is_Transiting]())
            {
                return;
            }

            this.Item[ItemState.Item_Is_Transiting] = () => true;
            
            interop.Process.AttachProcess(new ViewItemTransitProcess(draggingItem, targetView));
        }

        #endregion

        #region Update

        public override void Update(GameTime time)
        {
            this.UpdateViewScroll();
            this.UpdateViewSelection();

            // To improve performance
            if (this.ItemLogic.IsActive)
            {
                this.UpdateViewSwap();
            }
        }

        protected virtual void UpdateViewScroll()
        {
            this.ItemLogic.Id = this.View.Items.IndexOf(this.Item);
        }

        /// <summary>
        /// Item active state determination based on selection and view state
        /// </summary>
        private void UpdateViewSelection()
        {
            if (this.ViewSelection.IsSelected(this.ItemLogic.Id))
            {
                // Unify mouse and keyboard selection
                if (!this.Item[ItemState.Item_Is_Selected]())
                {
                    this.ItemLogic.CommonSelectsIt();
                }

                this.Item[ItemState.Item_Is_Selected] = () => true;
            }
            else
            {
                // Unify mouse and keyboard selection
                if (this.Item[ItemState.Item_Is_Selected]())
                {
                    this.ItemLogic.CommonUnselectsIt();
                }

                this.Item[ItemState.Item_Is_Selected] = () => false;
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