namespace MetaMind.Engine.Core.Entity.Control.Views.Controllers
{
    using Item.Factories;
    using Layouts;
    using Scrolls;
    using Selections;
    using Swaps;

    public class MMPointViewController : MMViewController
    {
        protected MMPointViewController(
            IMMView view,
            IMMViewScrollController viewScroll,
            IMMViewSelectionController viewSelection,
            IMMViewSwapController viewSwap,
            IMMViewLayout viewLayout,
            IViewItemFactory itemFactory)
            : base(view, viewScroll, viewSelection, viewSwap, viewLayout, itemFactory)
        {
        }
    }
}