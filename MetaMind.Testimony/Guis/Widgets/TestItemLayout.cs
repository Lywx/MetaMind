namespace MetaMind.Testimony.Guis.Widgets
{
    using Engine.Guis.Widgets.Items;
    using Engine.Guis.Widgets.Items.Interactions;
    using Engine.Guis.Widgets.Items.Layouts;

    public class TestItemLayout : BlockViewVerticalItemLayout
    {
        public TestItemLayout(
            IViewItem item,
            IViewItemLayoutInteraction itemLayoutInteraction)
            : base(item, itemLayoutInteraction)
        {
        }

        protected override void UpdateBlockRow()
        {
            // Remove the last empty string element by - 1
            // Added the position for name frame by + 1
            // Added the position for name frame by + 1
            this.BlockRow = this.BlockStringWrapped.Split('\n').Length - 1 + 1;
        }
    }
}