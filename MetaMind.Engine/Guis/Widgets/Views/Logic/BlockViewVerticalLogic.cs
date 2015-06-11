namespace MetaMind.Engine.Guis.Widgets.Views.Logic
{
    using Items.Factories;
    using Layouts;
    using Scrolls;
    using Selections;
    using Swaps;

    public class BlockViewVerticalLogic : PointViewVerticalLogic, IBlockViewVerticalLogic
    {
        public BlockViewVerticalLogic(
            IView view,
            IViewScrollController viewScroll,
            IViewSelectionController viewSelection,
            IViewSwapController viewSwap,
            IViewLayout viewLayout,
            IViewItemFactory itemFactory)
            : base(view, viewScroll, viewSelection, viewSwap, viewLayout, itemFactory)
        {
        }

        public new IBlockViewVerticalScrollController ViewScroll
        {
            get { return (IBlockViewVerticalScrollController)base.ViewScroll; }
        }

        public new IBlockViewVerticalSelectionController ViewSelection
        {
            get
            {
                return (IBlockViewVerticalSelectionController)base.ViewSelection;
            }
        }
    }
}