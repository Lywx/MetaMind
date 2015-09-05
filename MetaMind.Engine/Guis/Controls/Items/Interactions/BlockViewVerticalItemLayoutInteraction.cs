namespace MetaMind.Engine.Guis.Widgets.Items.Interactions
{
    using System;
    using Layouts;
    using Views.Scrolls;
    using Views.Selections;

    public class BlockViewVerticalItemLayoutInteraction : ViewItemComponent, IViewItemLayoutInteraction
    {
        private readonly IViewSelectionController viewSelection;

        private readonly IViewScrollController viewScroll;

        public BlockViewVerticalItemLayoutInteraction(
            IViewItem item,
            IViewSelectionController          viewSelection,
            BlockViewVerticalScrollController viewScroll)
            : base(item)
        {
            if (viewSelection == null)
            {
                throw new ArgumentNullException("viewSelection");
            }

            if (viewScroll == null)
            {
                throw new ArgumentNullException("viewScroll");
            }

            this.viewSelection = viewSelection;
            this.viewScroll = viewScroll;
        }

        public void ViewDoSelect(IViewItemLayout itemLayout)
        { 
            this.viewSelection.Select(itemLayout.Id);
        }

        public void ViewDoUnselect(IViewItemLayout itemLayout)
        {
            if (this.viewSelection.IsSelected(itemLayout.Id))
            {
                this.viewSelection.Cancel();
            }
        }

        public bool ViewCanDisplay(IViewItemLayout itemLayout)
        {
            return this.viewScroll.CanDisplay(((IPointViewItemLayout)itemLayout).Row);
        }
    }
}