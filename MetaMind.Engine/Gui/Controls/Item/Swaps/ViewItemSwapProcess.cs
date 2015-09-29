// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewItemSwapProcess.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Gui.Controls.Item.Swaps
{
    using System;
    using Engine.Components.Interop.Process;
    using Extensions;
    using Logic;
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
            IViewItem      draggingItem, 
            IViewItemLogic draggingItemLogic, 
            IViewLogic     draggingViewLogic, 
            IViewItem      swappingItem, 
            IViewItemLogic swappingItemLogic, 
            IViewLogic     swappingViewLogic)
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

            if (draggingViewLogic == null)
            {
                throw new ArgumentNullException("draggingViewLogic");
            }

            if (swappingItem == null)
            {
                throw new ArgumentNullException("swappingItem");
            }

            if (swappingItemLogic == null)
            {
                throw new ArgumentNullException("swappingItemLogic");
            }

            if (swappingViewLogic == null)
            {
                throw new ArgumentNullException("swappingViewLogic");
            }

            this.DraggingItem      = draggingItem;
            this.DraggingItemLogic = draggingItemLogic;
            this.DraggingViewLogic = draggingViewLogic;

            this.SwappingItem      = swappingItem;
            this.SwappingItemLogic = swappingItemLogic;
            this.SwappingViewLogic = swappingViewLogic;

            this.inSameView = ReferenceEquals(
                this.SwappingItem.View,
                this.DraggingItem.View);

            this.swappingItemIsMouseOver = this.SwappingItem[ViewItemState.Item_Is_Mouse_Over];
            this.swappingViewHasFocus    = this.SwappingItem.View[ViewState.View_Has_Focus];
            this.draggingViewHasFocus    = this.DraggingItem.View[ViewState.View_Has_Focus];

            // Temporarily disable the mouse over state (without affecting the inner working of the underlying frames)
            // during swapping to avoid possible re-swapping behavior.
            this.SwappingItem[ViewItemState.Item_Is_Mouse_Over] = () => false;
        }

        #endregion

        #region Dependency

        protected IViewItem SwappingItem { get; private set; }

        protected IViewItemLogic SwappingItemLogic { get; set; }

        protected IViewLogic DraggingViewLogic { get; set; }

        protected IViewItem DraggingItem { get; private set; }

        protected IViewItemLogic DraggingItemLogic { get; set; }

        protected IViewLogic SwappingViewLogic { get; set; }

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
            this.SwappingViewLogic.ViewSwap.Progress = (float)Math.Pow(this.Progress, 2);
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
            this.DraggingViewLogic.ViewSelection.Cancel();
            this.DraggingItem.View[ViewState.View_Has_Focus] = () => false;
            this.SwappingViewLogic.ViewSelection.Select(0);
            this.SwappingItem.View[ViewState.View_Has_Focus] = () => true;

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
            ((ViewItem)this.SwappingItem).OnSwapped();

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
            this.SwappingItem[ViewItemState.Item_Is_Mouse_Over] = this.swappingItemIsMouseOver;
            this.SwappingItem.View[ViewState.View_Has_Focus] = this.swappingViewHasFocus;
            this.DraggingItem.View[ViewState.View_Has_Focus] = this.draggingViewHasFocus;

            this.SwappingItem[ViewItemState.Item_Is_Swapped] = () => false;
        }

        #endregion
    }
}