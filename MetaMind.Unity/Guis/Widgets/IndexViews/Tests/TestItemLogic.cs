namespace MetaMind.Unity.Guis.Widgets.IndexViews.Tests
{
    using Engine.Gui.Controls.Item;
    using Engine.Gui.Controls.Item.Data;
    using Engine.Gui.Controls.Item.Interactions;
    using Engine.Gui.Controls.Item.Layouts;
    using Engine.Gui.Controls.Item.Logic;

    public class TestItemLogic : IndexBlockViewVerticalItemLogic
    {
        public TestItemLogic(
            IViewItem            item,
            TestItemFrameController    itemFrame,
            IViewItemInteraction itemInteraction,
            IViewItemDataModel   itemModel,
            IViewItemLayout      itemLayout)
            : base(item, itemFrame, itemInteraction, itemModel, itemLayout)
        {
        }

        public new TestItemFrameController ItemFrame
        {
            get { return (TestItemFrameController)base.ItemFrame; }
        }

        public override void Initialize()
        {
            base.Initialize();

            this.ItemFrame.NameRectangle       .MousePressLeft  += (o, args) => this.ToggleIndexView();
            this.ItemFrame.DescriptionRectangle.MousePressLeft  += (o, args) => this.ToggleIndexView();
            this.ItemFrame.RootRectangle       .MousePressRight += (o, args) => this.SelectPath();
        }
    }
}