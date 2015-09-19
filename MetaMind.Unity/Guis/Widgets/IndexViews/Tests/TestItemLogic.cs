namespace MetaMind.Unity.Guis.Widgets.IndexViews.Tests
{
    using Engine.Gui.Control.Item;
    using Engine.Gui.Control.Item.Data;
    using Engine.Gui.Control.Item.Interactions;
    using Engine.Gui.Control.Item.Layouts;
    using Engine.Gui.Control.Item.Logic;

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