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

    using MetaMind.Engine.Components.Processes;
    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Guis.Widgets.Items.Logic;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Guis.Widgets.Views.Logic;

    using Microsoft.Xna.Framework;

    public class ViewItemSwapProcess<T> : Process
    {
        private const int UpdateNum = 6;

        #region Constructors

        public ViewItemSwapProcess(
            IViewItem      draggingItem, 
            IViewItemLogic draggingItemLogic, 
            IViewLogic     draggingViewLogic, 
            IViewItem      swappingItem, 
            IViewItemLogic swappingItemLogic, 
            IViewLogic     swappingViewLogic, 
            IList<T> commonSource = null)
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
        }

        #endregion

        #region Dependency

        protected IViewItemLogic SwappingItemLogic { get; set; }

        protected IViewLogic SwappingViewLogic { get; set; }

        protected IViewLogic DraggingViewLogic { get; set; }

        protected IViewItem DraggingItem { get; private set; }

        protected IViewItemLogic DraggingItemLogic { get; set; }

        protected IViewItem SwappingItem { get; private set; }

        protected IList<T> CommonSource { get; set; }

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

        #endregion

        #region ViewSwap Update

        public override void Update(GameTime time)
        {
            this.SwappingViewLogic.ViewSwap.Progress += 1f / UpdateNum;

            if (this.SwappingViewLogic.ViewSwap.Progress > 1)
            {
                this.Succeed();
            }
        }

        #endregion

        #region ViewSwap Operations

        protected virtual void SwapAroundView()
        {
            // Replace each another in their origial view
            this.DraggingViewLogic.ViewSelection.Cancel();
            this.DraggingItem.View[ViewState.View_Has_Focus] = () => false;

            this.SwappingViewLogic.ViewSelection.Select(0);
            this.SwappingItem.View[ViewState.View_Has_Focus] = () => true;

            this.SwappingItem.View.Items.SwapWith(
                this.SwappingItem.View.Items, 
                this.SwappingItemLogic.ItemLayout.Id, 
                this.DraggingItemLogic.ItemLayout.Id);
        }

        protected virtual void SwapDataInList()
        {
            if (this.CommonSource == null || 
               !this.CommonSource.Contains(this.DraggingItem.ItemData) || 
               !this.CommonSource.Contains(this.SwappingItem.ItemData))
            {
                return;
            }

            this.CommonSource.Swap(
                (int)this.CommonSource.IndexOf(this.DraggingItem.ItemData), 
                (int)this.CommonSource.IndexOf(this.SwappingItem.ItemData));
        }

        protected virtual void SwapInView()
        {
            // Swap id then sort
            var tempId = this.SwappingItemLogic.ItemLayout.Id;
            this.SwappingItemLogic.ItemLayout.Id = this.DraggingItemLogic.ItemLayout.Id;
            this.DraggingItemLogic.ItemLayout.Id = tempId;

            this.SwappingViewLogic.ViewLayout.Sort(item => item.ItemLogic.ItemLayout.Id);
        }

        protected void SwapTerminate()
        {
            // Refine selection to make sure the overall effect is smooth
            this.DraggingItemLogic.ItemInteraction.ViewSelect();

            // Stop swapping state
            this.SwappingItem[ItemState.Item_Is_Swaping] = () => false;
        }

        #endregion
    }
}