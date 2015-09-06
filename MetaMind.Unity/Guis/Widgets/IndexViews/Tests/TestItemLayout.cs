namespace MetaMind.Unity.Guis.Widgets.IndexViews.Tests
{
    using Engine.Guis.Controls.Items;
    using Engine.Guis.Controls.Items.Interactions;
    using Engine.Guis.Controls.Items.Layouts;

    public class TestItemLayout : IndexBlockViewVerticalItemLayout
    {
        public TestItemLayout(
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