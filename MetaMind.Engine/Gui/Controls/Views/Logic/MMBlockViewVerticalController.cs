namespace MetaMind.Engine.Gui.Controls.Views.Logic
{
    using Item.Factories;
    using Layouts;
    using Scrolls;
    using Selections;
    using Swaps;

    public class MMBlockViewVerticalController : MMPointViewVerticalController, IMMBlockViewVerticalController
    {
        public MMBlockViewVerticalController(
            IMMViewNode view,
            IViewScrollController viewScroll,
            IViewSelectionController viewSelection,
            IViewSwapController viewSwap,
            IViewLayout viewLayout,
            IViewItemFactory itemFactory)
            : base(view, viewScroll, viewSelection, viewSwap, viewLayout, itemFactory)
        {
        }

        public new IBlockViewVerticalScrollController ViewScroll => (IBlockViewVerticalScrollController)base.ViewScroll;

        public new IBlockViewVerticalSelectionController ViewSelection => (IBlockViewVerticalSelectionController)base.ViewSelection;
    }
}