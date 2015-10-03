namespace MetaMind.Engine.Gui.Controls.Views.Logic
{
    using Item.Factories;
    using Layouts;
    using Scrolls;
    using Selections;
    using Swaps;

    public class IndexBlockViewVerticalLogic : BlockViewVerticalLogic
    {
        public IndexBlockViewVerticalLogic(
            IMMViewNode view,
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