namespace MetaMind.Engine.Entities.Controls.Views.Logic
{
    using Item.Factories;
    using Layouts;
    using Scrolls;
    using Selections;
    using Swaps;

    public class MMIndexedBlockViewVerticalController : MMIndexBlockViewVerticalController
    {
        public MMIndexedBlockViewVerticalController(
            IMMView view,
            IMMViewScrollController viewScroll,
            IMMViewSelectionController viewSelection,
            IMMViewSwapController viewSwap,
            IMMViewLayout viewLayout,
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