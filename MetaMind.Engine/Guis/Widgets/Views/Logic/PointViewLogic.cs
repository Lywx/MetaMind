namespace MetaMind.Engine.Guis.Widgets.Views.Logic
{
    using System.Collections.Generic;
    using Items.Data;
    using Items.Factories;
    using Layouts;
    using Scrolls;
    using Selections;
    using Swaps;

    public class PointViewLogic<TData> : ViewLogic<TData>
    {
        protected PointViewLogic(
            IView                    view,
            IList<TData>             viewData,
            IViewScrollController    viewScroll,
            IViewSelectionController viewSelection,
            IViewSwapController<TData> viewSwap,
            IViewLayout              viewLayout,
            IViewItemBinding<TData> itemBinding,
            IViewItemFactory itemFactory)
            : base(view, viewData, viewScroll, viewSelection, viewSwap, viewLayout, itemBinding, itemFactory)
        {
        }
    }
}