namespace MetaMind.Engine.Guis.Controls.Views.Logic
{
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
            IViewItemFactory itemFactory)
            : base(view, viewScroll, viewSelection, viewSwap, viewLayout, itemFactory)
        {
        }
    }
}