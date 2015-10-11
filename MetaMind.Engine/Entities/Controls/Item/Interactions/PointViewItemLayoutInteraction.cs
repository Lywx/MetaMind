namespace MetaMind.Engine.Entities.Controls.Item.Interactions
{
    using System;
    using Layouts;
    using Views.Scrolls;
    using Views.Selections;

    /// <summary>
    /// A interface class connects the view selection and view scroll with item layout
    /// </summary>
    public class MMPointViewItemLayoutInteraction : MMViewItemControllerComponent, IViewItemLayoutInteraction
    {
        private readonly IMMViewSelectionController viewSelection;

        private readonly IMMViewScrollController viewScroll;

        public MMPointViewItemLayoutInteraction(IMMViewItem item, IMMViewSelectionController viewSelection, IMMViewScrollController viewScroll)
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
            return this.viewScroll.CanDisplay(itemLayout.Id);
        }
    }
}