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

    public class ViewItemSwapProcess<TData> : StepProcess
    {
        private readonly Func<bool> swappingItemIsMouseOver;

        #region Constructors

        public ViewItemSwapProcess(
            IViewItem      draggingItem, 
            IViewItemLogic draggingItemLogic, 
            IViewLogic     draggingViewLogic, 
            IViewItem      swappingItem, 
            IViewItemLogic swappingItemLogic, 
            IViewLogic     swappingViewLogic, 
            IList<TData> commonSource = null)
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

            this.CommonSource = commonSource;

            this.swappingItemIsMouseOver = this.SwappingItem[ItemState.Item_Is_Mouse_Over];
            this.SwappingItem[ItemState.Item_Is_Mouse_Over] = () => false;
        }

        #endregion

        #region Dependency

        /// <summary>
        /// Temporary storage of original Func<bool>, which is temporarily and intentionally forced 
        /// to false during swapping to avoid possible re-swapping behavior.
        /// </summary>
        protected Func<bool> SwappingItemIsMouseOver
        {
            get { return this.swappingItemIsMouseOver; }
        }

        protected IViewItemLogic SwappingItemLogic { get; set; }

        protected IViewLogic SwappingViewLogic { get; set; }

        protected IViewLogic DraggingViewLogic { get; set; }

        protected IViewItem DraggingItem { get; private set; }

        protected IViewItemLogic DraggingItemLogic { get; set; }

        protected IViewItem SwappingItem { get; private set; }

        /// <summary>
        /// Data model to manipulate data collection.
        /// </summary>
        /// <remarks>
        /// I saw this IList<T> as a data model. It maintains a collection of data and 
        /// it is easy to provide management method as extension method.
        /// </remarks>>
        protected IList<TData> CommonSource { get; set; }

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
                var inSameView = ReferenceEquals(this.SwappingItem.View, this.DraggingItem.View);
                if (inSameView)
                {
                    this.SwapInView();
                }
                else
                {
                    this.SwapAroundView();
                }

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

        protected virtual void SwapAroundView()
        {
            this.SwapDataInList();

            // Replace each another in their original view
            this.DraggingViewLogic.ViewSelection.Cancel();
            this.DraggingItem.View[ViewState.View_Has_Focus] = () => false;

            this.SwappingViewLogic.ViewSelection.Select(0);
            this.SwappingItem.View[ViewState.View_Has_Focus] = () => true;

            this.SwapItemAroundList();
        }

        protected virtual void SwapDataInList()
        {
            this.CommonSource.Swap(
                (int)this.CommonSource.IndexOf(this.DraggingItem.ItemData), 
                (int)this.CommonSource.IndexOf(this.SwappingItem.ItemData));
        }

        protected virtual void SwapInView()
        {
            this.SwapDataInList();
            this.SwapItemInList();
        }

        private void SwapItemInList()
        {
            this.SwappingItem.View.ItemsWrite.Swap(
                SwappingItemLogic.ItemLayout.Id,
                DraggingItemLogic.ItemLayout.Id);
        }

        private void SwapItemAroundList()
        {
            this.SwappingItem.View.ItemsRead.SwapWith(
                this.SwappingItem.View.ItemsRead,
                this.SwappingItemLogic.ItemLayout.Id,
                this.DraggingItemLogic.ItemLayout.Id);
        }

        protected void SwapTerminate()
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

        protected void SwapFinalize()
        {
            // Restore original Func<bool>
            this.SwappingItem[ItemState.Item_Is_Mouse_Over] = this.SwappingItemIsMouseOver;
            this.SwappingItem[ItemState.Item_Is_Swapped] = () => false;
        }

        #endregion
    }
}