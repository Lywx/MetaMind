// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewSwap.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Gui.Controls.Views.Swaps
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using Item;
    using Item.Swaps;
    using Microsoft.Xna.Framework;
    using Services;

    public class ViewSwapController : MMViewControlComponent, IViewSwapController
    {
        private readonly List<IMMViewNode> viewObservers;

        #region Constructors

        public ViewSwapController(IMMViewNode view)
            : base(view)
        {
            this.viewObservers = new List<IMMViewNode>();
        }

        #endregion

        #region Cross View 

        public void AddObserver(IMMViewNode view)
        {
            this.viewObservers.Add(view);
        }

        public void RemoveObserver(IMMViewNode view)
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

        public virtual void StartProcess(IMMEngineInteropService interop, IViewItem touchedItem, Vector2 touchedStart, IViewItem draggingItem, IMMViewNode draggingView, Vector2 draggingEnd)
        {
            this.HasStarted = true;
            this.Progress   = 0f;

            this.Start = touchedStart;
            this.End   = draggingEnd;

            ((ViewItem)touchedItem).OnSwapping();

            interop.Process.AttachProcess(new ViewItemSwapProcess(
                draggingItem,
                draggingItem.ItemLogic,
                draggingView.ViewController,
                touchedItem,
                touchedItem.ItemLogic,
                this.View.ViewController));
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
        private void WatchFrom(IViewItem draggingItem, IMMViewNode touchedView)
        {
            Predicate<IViewItem> isActive  = t => t[ViewItemState.Item_Is_Active]();
            Predicate<IViewItem> isTouched = t => t[ViewItemState.Item_Is_Mouse_Over]();
            Predicate<IViewItem> isAnother = t => !ReferenceEquals(t, draggingItem);

            var swappingItem = touchedView.ItemsRead.
                FindAll(isActive).
                FindAll(isTouched).
                Find(isAnother);

            if (swappingItem != null && 

               // Avoid repetitive swapping when swapping has not finished
               !swappingItem[ViewItemState.Item_Is_Swaping]() && 
               
               // Avoid repetitive swapping when swapping is just finished
               !swappingItem[ViewItemState.Item_Is_Swapped]())
            {
                var swappingItemInteraction = swappingItem.ItemLogic.ItemInteraction;
                swappingItemInteraction.ViewSwap(this.Interop, draggingItem);
            }
        }

    }
}