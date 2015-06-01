namespace MetaMind.Engine.Guis.Widgets.Items.Interactions
{
    using MetaMind.Engine.Guis.Widgets.Items.Layouts;
    using MetaMind.Engine.Guis.Widgets.Views.Scrolls;
    using MetaMind.Engine.Guis.Widgets.Views.Selections;

    public class ViewItemLayoutInteraction : ViewItemComponent, IViewItemLayoutInteraction
    {
        private readonly IViewSelectionController viewSelection;

        private readonly IViewScrollController viewScroll;

        public ViewItemLayoutInteraction(IViewItem item, IViewSelectionController viewSelection, IViewScrollController viewScroll)
            : base(item)
        {
            this.viewSelection = viewSelection;
            this.viewScroll    = viewScroll;
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
            return this.viewScroll.CanDisplay(itemLayout.Id);
        }
    }
}