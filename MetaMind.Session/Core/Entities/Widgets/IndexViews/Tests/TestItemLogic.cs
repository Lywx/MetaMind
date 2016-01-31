namespace MetaMind.Session.Guis.Widgets.IndexViews.Tests
{
    using Engine.Core.Entity.Control.Item;
    using Engine.Core.Entity.Control.Item.Controllers;
    using Engine.Core.Entity.Control.Item.Data;
    using Engine.Core.Entity.Control.Item.Interactions;
    using Engine.Core.Entity.Control.Item.Layouts;

    public class TestItemLogic : MMIndexBlockViewVerticalItemLogic
    {
        public TestItemLogic(
            IMMViewItem            item,
            TestItemFrameController    itemFrame,
            IMMViewItemInteraction itemInteraction,
            IMMViewItemDataModel   itemModel,
            IMMViewItemLayout      itemLayout)
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

            this.ItemFrame.NameImmRectangle       .MousePressLeft  += (o, args) => this.ToggleIndexView();
            this.ItemFrame.DescriptionImmRectangle.MousePressLeft  += (o, args) => this.ToggleIndexView();
            this.ItemFrame.RootImmRectangle       .MousePressRight += (o, args) => this.SelectPath();
        }
    }
}