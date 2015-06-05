// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewSwap.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Views.Swaps
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    using Items;
    using Items.Swaps;
    using Services;

    using Microsoft.Xna.Framework;

    public class ViewSwapController<TData> : ViewComponent, IViewSwapController
    {
        /// <remarks>
        /// View data model as a collections.
        /// </remarks>>
        private readonly IList<TData> viewData;

        private readonly List<IView> viewObservers;

        #region Constructors

        public ViewSwapController(IView view, IList<TData> viewData)
            : base(view)
        {
            if (viewData == null)
            {
                throw new ArgumentNullException("viewData");
            }

            this.viewData = viewData;

            this.viewObservers = new List<IView>();
        }

        #endregion

        #region Protected Properties

        /// <remarks>
        /// View data model as a collections.
        /// </remarks>>
        protected IList<TData> ViewData
        {
            get { return this.viewData; }
        }

        #endregion

        #region Cross View 

        public void AddObserver(IView view)
        {
            this.viewObservers.Add(view);
        }

        public void RemoveObserver(IView view)
        {
            this.viewObservers.Remove(view);
        }

        #endregion

        #region States

        protected bool HasStarted { get; set; }

        public float Progress { get; set; }

        protected Vector2 End { get; set; }

        protected Vector2 Start { get; set; }

        /// <summary>
        /// Linear straight line movement between start and end.
        /// </summary>
        /// <returns></returns>
        public Vector2 Position
        {
            get
            {
#if DEBUG
                Debug.Assert(this.HasStarted, "Process has not be started yet.");
#endif
                return this.Start + (this.End - this.Start) * this.Progress;
            }
        }

        #endregion

        public virtual void StartProcess(IGameInteropService interop, IViewItem touchedItem, Vector2 touchedStart, IViewItem draggingItem, IView draggingView, Vector2 draggingEnd)
        {
            this.HasStarted = true;
            this.Progress   = 0f;

            this.Start = touchedStart;
            this.End   = draggingEnd;

            ((ViewItem)touchedItem).OnSwapping();

            interop.Process.AttachProcess(new ViewItemSwapProcess<TData>(
                draggingItem,
                draggingItem.ItemLogic,
                draggingView.ViewLogic,
                touchedItem,
                touchedItem.ItemLogic,
                this.View.ViewLogic,
                this.ViewData));
        }

        public void WatchProcess(IViewItem item)
        {
            this.WatchFromSelf(item);
            this.WatchFromObserver(item);
        }

        private void WatchFromSelf(IViewItem item)
        {
            this.WatchFrom(item, this.View);
        }

        private void WatchFromObserver(IViewItem item)
        {
            foreach (var viewObserver in this.viewObservers)
            {
                this.WatchFrom(item, viewObserver);
            }
        }

        /// <summary>
        /// Watching possible dragging item swapping in target view.
        /// </summary>
        private void WatchFrom(IViewItem draggingItem, IView touchedView)
        {
            Predicate<IViewItem> isActive  = t => t[ItemState.Item_Is_Active]();
            Predicate<IViewItem> isTouched = t => t[ItemState.Item_Is_Mouse_Over]();
            Predicate<IViewItem> isAnother = t => !ReferenceEquals(t, draggingItem);

            var swappingItem = touchedView.ItemsRead.
                FindAll(isActive).
                FindAll(isTouched).
                Find(isAnother);

            if (swappingItem != null && 

               // Avoid repetitive swapping when swapping has not finished
               !swappingItem[ItemState.Item_Is_Swaping]() && 
               
               // Avoid repetitive swapping when swapping is just finished
               !swappingItem[ItemState.Item_Is_Swapped]())
            {
                var swappingItemInteraction = swappingItem.ItemLogic.ItemInteraction;
                swappingItemInteraction.ViewSwap(this.GameInterop, draggingItem);
            }
        }

    }
}