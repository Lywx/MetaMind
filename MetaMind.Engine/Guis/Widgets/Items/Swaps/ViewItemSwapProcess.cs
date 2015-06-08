// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewItemSwapProcess.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Items.Swaps
{
    using System;
    using System.Collections.Generic;

    using Components.Processes;
    using Extensions;
    using Logic;
    using Views;
    using Views.Logic;

    using Microsoft.Xna.Framework;

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
            IViewLogic     swappingViewLogic, 
            IList<dynamic> dataList = null)
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

            this.DataList = dataList;

            this.inSameView = ReferenceEquals(
                this.SwappingItem.View,
                this.DraggingItem.View);

            this.swappingItemIsMouseOver = this.SwappingItem[ItemState.Item_Is_Mouse_Over];
            this.swappingViewHasFocus    = this.SwappingItem.View[ViewState.View_Has_Focus];
            this.draggingViewHasFocus    = this.DraggingItem.View[ViewState.View_Has_Focus];

            // Temporarily disable the mouse over state (without affecting the inner working of the underlying frames)
            // during swapping to avoid possible re-swapping behavior.
            this.SwappingItem[ItemState.Item_Is_Mouse_Over] = () => false;
        }

        #endregion

        #region Dependency

        protected IViewItem SwappingItem { get; private set; }

        protected IViewItemLogic SwappingItemLogic { get; set; }

        protected IViewLogic DraggingViewLogic { get; set; }

        protected IViewItem DraggingItem { get; private set; }

        protected IViewItemLogic DraggingItemLogic { get; set; }

        protected IViewLogic SwappingViewLogic { get; set; }

        /// <summary>
        /// Data model to manipulate data collection.
        /// </summary>
        /// <remarks>
        /// I saw this IList<T> as a data model. It maintains a collection of data and 
        /// it is easy to provide management method as extension method.
        /// </remarks>>
        protected IList<dynamic> DataList { get; set; }

        #endregion

        #region ViewSwap Transition

        public override void OnAbort()
        {
        }

        public override void OnFail()
        {
        }

        public override void OnSuccess()
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
            this.SwapDataInList();

            // Temporarily take control of view focus state
            this.DraggingViewLogic.ViewSelection.Cancel();
            this.DraggingItem.View[ViewState.View_Has_Focus] = () => false;
            this.SwappingViewLogic.ViewSelection.Select(0);
            this.SwappingItem.View[ViewState.View_Has_Focus] = () => true;

            this.SwapItemAroundList();
        }

        protected virtual void SwapInView()
        {
            this.SwapDataInList();
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
            this.SwappingItem.View.ItemsRead.SwapWith(
                this.SwappingItem.View.ItemsRead,
                this.SwappingItemLogic.ItemLayout.Id,
                this.DraggingItemLogic.ItemLayout.Id);
        }

        protected virtual void SwapDataInList()
        {
            this.DataList.Swap(
                (int)this.DataList.IndexOf(this.DraggingItem.ItemData), 
                (int)this.DataList.IndexOf(this.SwappingItem.ItemData));
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
            this.SwappingItem[ItemState.Item_Is_Mouse_Over] = this.swappingItemIsMouseOver;
            this.SwappingItem.View[ViewState.View_Has_Focus] = this.swappingViewHasFocus;
            this.DraggingItem.View[ViewState.View_Has_Focus] = this.draggingViewHasFocus;

            this.SwappingItem[ItemState.Item_Is_Swapped] = () => false;
        }

        #endregion
    }
}