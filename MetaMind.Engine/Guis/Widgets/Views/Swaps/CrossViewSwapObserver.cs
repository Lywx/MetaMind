namespace MetaMind.Engine.Guis.Widgets.Views.Swaps
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Views.Logic;

    public class CrossViewSwapObserver : ViewComponent, ICrossViewSwapObservor
    {
        public CrossViewSwapObserver(IView view)
            : base(view)
        {
        }

        /// <summary>
        /// Watching possible dragging item swapping in target view.
        /// </summary>
        /// <remarks>
        /// Valid universally.
        /// </remarks>
        public void WatchSwapFrom(IViewItem draggedItem, IView touchedView, IViewLogic touchedViewLogic)
        {
            Predicate<IViewItem> isActive  = t => t[ItemState.Item_Is_Active]();
            Predicate<IViewItem> isTouched = t => t[ItemState.Item_Is_Mouse_Over]();
            Predicate<IViewItem> isAnother = t => !ReferenceEquals(t, draggedItem);

            var swappingItem = touchedView.Items.
                FindAll(isActive).
                FindAll(isTouched).
                Find(isAnother);

            if (swappingItem != null && 
               !swappingItem[ItemState.Item_Is_Swaping]())
            {
                swappingItem.ItemLogic.ItemInteraction.ViewSwap(this.GameInterop, draggedItem);
            }
        }
    }
}