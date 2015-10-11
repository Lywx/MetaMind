// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewItemSwapProcess.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Entities.Controls.Item.Swaps
{
    using System;
    using Components.Interop.Process;
    using Microsoft.Xna.Framework;
    using Views;
    using Views.Logic;

    public class ViewItemSwapProcess : StepProcess
    {
        private readonly Func<bool> swappingItemIsMouseOver;

        private readonly Func<bool> draggingViewHasFocus;

        private readonly Func<bool> swappingViewHasFocus;

        private readonly bool inSameView;

        #region Constructors

        public ViewItemSwapProcess(
            IMMViewItem      draggingItem, 
            IMMViewItemController draggingItemLogic, 
            IMMViewController     draggingViewController, 
            IMMViewItem      swappingItem, 
            IMMViewItemController swappingItemLogic, 
            IMMViewController     swappingViewController)
            : base(10)
        {
            if (draggingItem == null)
            {
                throw new ArgumentNullException("draggingItem");
            }

            if (draggingItemLogic == null)
            {
                throw new ArgumentNullException("draggingItemLogic");
            }

            if (draggingViewController == null)
            {
                throw new ArgumentNullException("draggingViewController");
            }

            if (swappingItem == null)
            {
                throw new ArgumentNullException("swappingItem");
            }

            if (swappingItemLogic == null)
            {
                throw new ArgumentNullException("swappingItemLogic");
            }

            if (swappingViewController == null)
            {
                throw new ArgumentNullException("swappingViewController");
            }

            this.DraggingItem      = draggingItem;
            this.DraggingItemLogic = draggingItemLogic;
            this.DraggingViewController = draggingViewController;

            this.SwappingItem      = swappingItem;
            this.SwappingItemLogic = swappingItemLogic;
            this.SwappingViewController = swappingViewController;

            this.inSameView = ReferenceEquals(
                this.SwappingItem.View,
                this.DraggingItem.View);

            this.swappingItemIsMouseOver = this.SwappingItem[MMViewItemState.Item_Is_Mouse_Over];
            this.swappingViewHasFocus    = this.SwappingItem.View[MMViewState.View_Has_Focus];
            this.draggingViewHasFocus    = this.DraggingItem.View[MMViewState.View_Has_Focus];

            // Temporarily disable the mouse over state (without affecting the inner working of the underlying frames)
            // during swapping to avoid possible re-swapping behavior.
            this.SwappingItem[MMViewItemState.Item_Is_Mouse_Over] = () => false;
        }

        #endregion

        #region Dependency

        protected IMMViewItem SwappingItem { get; private set; }

        protected IMMViewItemController SwappingItemLogic { get; set; }

        protected IMMViewController DraggingViewController { get; set; }

        protected IMMViewItem DraggingItem { get; private set; }

        protected IMMViewItemController DraggingItemLogic { get; set; }

        protected IMMViewController SwappingViewController { get; set; }

        #endregion

        #region ViewSwap Transition

        public override void OnAbort()
        {
        }

        public override void OnFail()
        {
        }

        public override void OnSucceed()
        {
        }

        #endregion

        #region ViewSwap Update

        public override void Update(GameTime time)
        {
            base.Update(time);
            this.UpdateVisual(time);

            if (this.CurrentFrame == this.LastFrame - 1)
            {
                this.Swap();
                this.SwapTerminate();
            }
            else if (this.CurrentFrame == this.LastFrame)
            {
                this.SwapFinalize();
                this.Succeed();
            }
        }

        protected void UpdateVisual(GameTime time)
        {
            this.SwappingViewController.ViewSwap.Progress = (float)Math.Pow(this.Progress, 2);
        }

        #endregion

        #region ViewSwap Operations

        private void Swap()
        {
            if (this.inSameView)
            {
                this.SwapInView();
            }
            else
            {
                this.SwapAroundView();
            }
        }

        protected virtual void SwapAroundView()
        {
            // Temporarily take control of view focus state
            this.DraggingViewController.ViewSelection.Cancel();
            this.DraggingItem.View[MMViewState.View_Has_Focus] = () => false;
            this.SwappingViewController.ViewSelection.Select(0);
            this.SwappingItem.View[MMViewState.View_Has_Focus] = () => true;

            this.SwapItemAroundList();
        }

        protected virtual void SwapInView()
        {
            this.SwapItemInList();
        }

        private void SwapItemInList()
        {
            this.SwappingItem.View.ItemsWrite.Swap(
                this.SwappingItemLogic.ItemLayout.Id,
                this.DraggingItemLogic.ItemLayout.Id);
        }

        private void SwapItemAroundList()
        {
            this.SwappingItem.View.ItemsWrite.SwapWith(
                this.SwappingItem.View.ItemsWrite,
                this.SwappingItemLogic.ItemLayout.Id,
                this.DraggingItemLogic.ItemLayout.Id);
        }

        private void SwapTerminate()
        {
            ((MMViewItem)this.SwappingItem).OnSwapped();

            // Reselect the dragging item to make sure the overall effect is smooth.
            // 
            // It is calling from swapping item logic, because the swapping item's old 
            // id will be the the dragging item's new id in next frame.
            // 
            // The selection is id based.

            this.SwappingItemLogic.ItemInteraction.ViewSelect();
        }

        private void SwapFinalize()
        {
            // Restore original states
            this.SwappingItem[MMViewItemState.Item_Is_Mouse_Over] = this.swappingItemIsMouseOver;
            this.SwappingItem.View[MMViewState.View_Has_Focus] = this.swappingViewHasFocus;
            this.DraggingItem.View[MMViewState.View_Has_Focus] = this.draggingViewHasFocus;

            this.SwappingItem[MMViewItemState.Item_Is_Swapped] = () => false;
        }

        #endregion
    }
}