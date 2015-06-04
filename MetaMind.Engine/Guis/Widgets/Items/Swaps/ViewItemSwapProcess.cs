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

    public class ViewItemSwapProcess<TData> : Process
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
            IList<TData> commonSource = null)
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
            //this.SwapDataInList();

            //// Replace each another in their original view
            //this.DraggingViewLogic.ViewSelection.Cancel();
            //this.DraggingItem.View[ViewState.View_Has_Focus] = () => false;

            //this.SwappingViewLogic.ViewSelection.Select(0);
            //this.SwappingItem.View[ViewState.View_Has_Focus] = () => true;

            //this.SwappingItem.View.Items.SwapWith(
            //    this.SwappingItem.View.Items, 
            //    this.SwappingItemLogic.ItemLayout.Id, 
            //    this.DraggingItemLogic.ItemLayout.Id);
        }

        protected virtual void SwapDataInList()
        {
            this.CommonSource.Swap(
                (int)this.CommonSource.IndexOf(this.DraggingItem.ItemData), 
                (int)this.CommonSource.IndexOf(this.SwappingItem.ItemData));
        }

        protected virtual void SwapInView()
        {
            //this.SwapDataInList();
            this.SwapItemInList();
            //this.SwappingViewLogic.ViewLayout.Sort(item => item.ItemLogic.ItemLayout.Id);

            // Stop swapping state
            this.SwappingItem[ItemState.Item_Is_Swaping] = () => false;

            // Refine selection to make sure the overall effect is smooth
            this.DraggingItemLogic.ItemInteraction.ViewSelect();

            //this.SwappingItem.UpdateView(new GameTime());
            //this.DraggingItem.UpdateView(new GameTime());
        }

        private void SwapItemInList()
        {
            //var temp = this.SwappingItemLogic.ItemLayout.Id;
            //this. = this.DraggingItemLogic.ItemLayout.Id;
            //this. = temp;
            this.SwappingItem.View.Items.Swap(SwappingItemLogic.ItemLayout.Id, DraggingItemLogic.ItemLayout.Id );
        }

        protected void SwapTerminate()
        {
        }

        #endregion
    }
}