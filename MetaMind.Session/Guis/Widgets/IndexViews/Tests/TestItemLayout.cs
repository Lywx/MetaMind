namespace MetaMind.Session.Guis.Widgets.IndexViews.Tests
{
    using Engine.Entities.Controls.Item;
    using Engine.Entities.Controls.Item.Interactions;
    using Engine.Entities.Controls.Item.Layouts;

    public class TestItemLayout : MMIndexBlockViewVerticalItemLayout
    {
        public TestItemLayout(
            IMMViewItem item,
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