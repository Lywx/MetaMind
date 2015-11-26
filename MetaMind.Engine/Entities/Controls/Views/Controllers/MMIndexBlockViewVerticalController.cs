namespace MetaMind.Engine.Entities.Controls.Views.Controllers
{
    using Item.Factories;
    using Layouts;
    using Scrolls;
    using Selections;
    using Swaps;

    public class MMIndexBlockViewVerticalController : MMBlockViewVerticalController
    {
        public MMIndexBlockViewVerticalController(
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