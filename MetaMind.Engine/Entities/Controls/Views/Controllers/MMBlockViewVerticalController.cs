namespace MetaMind.Engine.Entities.Controls.Views.Controllers
{
    using Item.Factories;
    using Layouts;
    using Scrolls;
    using Selections;
    using Swaps;

    public class MMBlockViewVerticalController : MMPointViewVerticalController, IMMBlockViewVerticalController
    {
        public MMBlockViewVerticalController(
            IMMView view,
            IMMViewScrollController viewScroll,
            IMMViewSelectionController viewSelection,
            IMMViewSwapController viewSwap,
            IMMViewLayout viewLayout,
            IViewItemFactory itemFactory)
            : base(view, viewScroll, viewSelection, viewSwap, viewLayout, itemFactory)
        {
        }

        public new IMMBlockViewVerticalScrollController ViewScroll => (IMMBlockViewVerticalScrollController)base.ViewScroll;

        public new IMMBlockViewVerticalSelectionController ViewSelection => (IMMBlockViewVerticalSelectionController)base.ViewSelection;
    }
}