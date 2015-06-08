namespace MetaMind.Engine.Guis.Widgets.Views.Logic
{
    using System.Collections.Generic;
    using Items.Data;
    using Items.Factories;
    using Layouts;
    using Scrolls;
    using Selections;
    using Swaps;

    public class PointViewLogic : ViewLogic
    {
        protected PointViewLogic(
            IView view,
            IViewScrollController viewScroll,
            IViewSelectionController viewSelection,
            IViewSwapController viewSwap,
            IViewLayout viewLayout,
            IViewItemBinding itemBinding,
            IViewItemFactory itemFactory)
            : base(view, viewScroll, viewSelection, viewSwap, viewLayout, itemBinding, itemFactory)
        {
        }
    }
}