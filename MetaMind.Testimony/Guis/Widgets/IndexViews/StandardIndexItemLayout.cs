namespace MetaMind.Testimony.Guis.Widgets.IndexViews
{
    using Engine.Guis.Widgets.Items;
    using Engine.Guis.Widgets.Items.Interactions;
    using Engine.Guis.Widgets.Items.Layouts;

    public class StandardIndexItemLayout : IndexBlockViewVerticalItemLayout
    {
        public StandardIndexItemLayout(
            IViewItem item,
            IViewItemLayoutInteraction itemLayoutInteraction)
            : base(item, itemLayoutInteraction)
        {
        }

        protected override void UpdateBlockRow()
        {
            this.BlockRow = this.BlockStringWrapped.Split('\n').Length 
            // Remove the last empty string element by - 1
                - 1 
            // Added the position for name frame by + 1
                + 1;
        }
    }
}