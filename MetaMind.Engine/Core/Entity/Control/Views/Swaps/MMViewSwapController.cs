namespace MetaMind.Engine.Core.Entity.Control.Views.Swaps
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using Item;
    using Item.Swaps;
    using Microsoft.Xna.Framework;

    public class MMViewSwapController : MMViewControlComponent, IMMViewSwapController
    {
        private readonly List<IMMView> viewObservers;

        #region Constructors

        public MMViewSwapController(IMMView view)
            : base(view)
        {
            this.viewObservers = new List<IMMView>();
        }

        #endregion

        #region Cross View 

        public void AddObserver(IMMView view)
        {
            this.viewObservers.Add(view);
        }

        public void RemoveObserver(IMMView view)
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

        public virtual void StartProcess(IMMViewItem touchedItem, Vector2 touchedStart, IMMViewItem draggingItem, IMMView draggingView, Vector2 draggingEnd)
        {
            this.HasStarted = true;
            this.Progress   = 0f;

            this.Start = touchedStart;
            this.End   = draggingEnd;

            ((MMViewItem)touchedItem).OnSwapping();

            this.GlobalInterop.Process.AttachProcess(new ViewItemSwapProcess(
                draggingItem,
                draggingItem.ItemLogic,
                draggingView.ViewController,
                touchedItem,
                touchedItem.ItemLogic,
                this.View.ViewController));
        }

        public void WatchProcess(IMMViewItem item)
        {
            this.WatchFromSelf(item);
            this.WatchFromObserver(item);
        }

        private void WatchFromSelf(IMMViewItem item)
        {
            this.WatchFrom(item, this.View);
        }

        private void WatchFromObserver(IMMViewItem item)
        {
            foreach (var viewObserver in this.viewObservers)
            {
                this.WatchFrom(item, viewObserver);
            }
        }

        /// <summary>
        /// Watching possible dragging item swapping in target view.
        /// </summary>
        private void WatchFrom(IMMViewItem draggingItem, IMMView touchedView)
        {
            Predicate<IMMViewItem> isActive  = t => t[MMViewItemState.Item_Is_Active]();
            Predicate<IMMViewItem> isTouched = t => t[MMViewItemState.Item_Is_Mouse_Over]();
            Predicate<IMMViewItem> isAnother = t => !ReferenceEquals(t, draggingItem);

            var swappingItem = touchedView.ItemsRead.
                FindAll(isActive).
                FindAll(isTouched).
                Find(isAnother);

            if (swappingItem != null && 

               // Avoid repetitive swapping when swapping has not finished
               !swappingItem[MMViewItemState.Item_Is_Swaping]() && 
               
               // Avoid repetitive swapping when swapping is just finished
               !swappingItem[MMViewItemState.Item_Is_Swapped]())
            {
                var swappingItemInteraction = swappingItem.ItemLogic.ItemInteraction;
                swappingItemInteraction.ViewSwap(this.GlobalInterop, draggingItem);
            }
        }

    }
}