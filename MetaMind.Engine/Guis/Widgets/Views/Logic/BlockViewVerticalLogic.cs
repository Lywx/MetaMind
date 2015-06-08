namespace MetaMind.Engine.Guis.Widgets.Views.Logic
{
    using System.Collections.Generic;
    using Items.Data;
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
            IViewItemBinding itemBinding,
            IViewItemFactory itemFactory)
            : base(view, viewScroll, viewSelection, viewSwap, viewLayout, itemBinding, itemFactory)
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