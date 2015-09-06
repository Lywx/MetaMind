namespace MetaMind.Unity.Guis.Widgets.IndexViews.Tests
{
    using Engine.Guis.Controls.Items;
    using Engine.Guis.Controls.Items.Data;
    using Engine.Guis.Controls.Items.Interactions;
    using Engine.Guis.Controls.Items.Layouts;
    using Engine.Guis.Controls.Items.Logic;

    public class TestItemLogic : IndexBlockViewVerticalItemLogic
    {
        public TestItemLogic(
            IViewItem            item,
            TestItemFrame    itemFrame,
            IViewItemInteraction itemInteraction,
            IViewItemDataModel   itemModel,
            IViewItemLayout      itemLayout)
            : base(item, itemFrame, itemInteraction, itemModel, itemLayout)
        {
        }

        public new TestItemFrame ItemFrame
        {
            get { return (TestItemFrame)base.ItemFrame; }
        }

        public override void SetupLayer()
        {
            base.SetupLayer();

            this.ItemFrame.NameFrame       .MouseLeftPressed  += (o, args) => this.ToggleIndexView();
            this.ItemFrame.DescriptionFrame.MouseLeftPressed  += (o, args) => this.ToggleIndexView();
            this.ItemFrame.RootFrame       .MouseRightPressed += (o, args) => this.SelectPath();
        }
    }
}