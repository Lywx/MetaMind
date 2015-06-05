namespace MetaMind.Engine.Guis.Widgets.Views.Logic
{
    using System.Collections.Generic;

    using Items.Factories;
    using Layouts;
    using Scrolls;
    using Selections;
    using Swaps;

    public class BlockViewVerticalLogic<TData> : PointViewVerticalLogic<TData>, IBlockViewVerticalLogic
    {
        protected BlockViewVerticalLogic(
            IView                    view,
            IList<TData>             viewData,
            IViewScrollController    viewScroll,
            IViewSelectionController viewSelection,
            IViewSwapController      viewSwap,
            IViewLayout              viewLayout,
            IViewItemFactory itemFactory)
            : base(view, viewData, viewScroll, viewSelection, viewSwap, viewLayout, itemFactory)
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