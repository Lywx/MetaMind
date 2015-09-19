namespace MetaMind.Engine.Gui.Control.Views.Logic
{
    using Item.Factories;
    using Layouts;
    using Scrolls;
    using Selections;
    using Swaps;

    public class IndexedBlockViewVerticalLogic : IndexBlockViewVerticalLogic
    {
        public IndexedBlockViewVerticalLogic(
            IView view,
            IViewScrollController viewScroll,
            IViewSelectionController viewSelection,
            IViewSwapController viewSwap,
            IViewLayout viewLayout,
            IViewItemFactory itemFactory)
            : base(view, viewScroll, viewSelection, viewSwap, viewLayout, itemFactory)
        {
        }

        /// <summary>
        /// Disable scrolling of index view for chances of inconsistent scrolling 
        /// effect with host view.
        /// </summary>
        public override void ScrollUp()
        {
        }

        /// <summary>
        /// Disable scrolling of index view for chances of inconsistent scrolling 
        /// effect with host view.
        /// </summary>
        public override void ScrollDown()
        {
        }
    }
}