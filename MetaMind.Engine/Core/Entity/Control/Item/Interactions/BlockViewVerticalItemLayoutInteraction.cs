namespace MetaMind.Engine.Core.Entity.Control.Item.Interactions
{
    using System;
    using Layouts;
    using Views.Scrolls;
    using Views.Selections;

    public class MMBlockViewVerticalItemLayoutInteraction : MMViewItemControllerComponent, IViewItemLayoutInteraction
    {
        private readonly IMMViewSelectionController viewSelection;

        private readonly IMMViewScrollController viewScroll;

        public MMBlockViewVerticalItemLayoutInteraction(
            IMMViewItem item,
            IMMViewSelectionController          viewSelection,
            MMBlockViewVerticalScrollController viewScroll)
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

        public void ViewDoSelect(IMMViewItemLayout itemLayout)
        { 
            this.viewSelection.Select(itemLayout.Id);
        }

        public void ViewDoUnselect(IMMViewItemLayout itemLayout)
        {
            if (this.viewSelection.IsSelected(itemLayout.Id))
            {
                this.viewSelection.Cancel();
            }
        }

        public bool ViewCanDisplay(IMMViewItemLayout itemLayout)
        {
            return this.viewScroll.CanDisplay(((IMMPointViewItemLayout)itemLayout).Row);
        }
    }
}