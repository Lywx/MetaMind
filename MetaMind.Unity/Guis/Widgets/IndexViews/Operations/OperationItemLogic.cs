namespace MetaMind.Unity.Guis.Widgets.IndexViews.Operations
{
    using Concepts.Operations;
    using Engine.Gui.Control.Item;
    using Engine.Gui.Control.Item.Data;
    using Engine.Gui.Control.Item.Frames;
    using Engine.Gui.Control.Item.Interactions;
    using Engine.Gui.Control.Item.Layouts;
    using Engine.Gui.Control.Item.Logic;

    public class OperationItemLogic : IndexBlockViewVerticalItemLogic 
    {
        public OperationItemLogic(
            IViewItem            item,
            IViewItemFrameController       itemFrame,
            IViewItemInteraction itemInteraction,
            IViewItemDataModel   itemModel,
            IViewItemLayout      itemLayout)
            : base(item, itemFrame, itemInteraction, itemModel, itemLayout)
        {
        }

        public new OperationItemFrameController ItemFrame
        {
            get { return (OperationItemFrameController)base.ItemFrame; }
        }

        public override void Initialize()
        {
            base.Initialize();

            this.ItemFrame.NameRectangle       .MousePressLeft  += (o, args) => this.ToggleIndexView();
            this.ItemFrame.DescriptionRectangle.MousePressLeft  += (o, args) => this.ToggleIndexView();
            this.ItemFrame.StatusRectangle     .MousePressLeft  += (o, args) => this.ToggleOperation();
            this.ItemFrame.RootRectangle       .MousePressRight += (o, args) => this.SelectPath();
        }

        #region Operations

        private void ToggleOperation()
        {
            ((IOperationDescription)this.Item.ItemData).Toggle();
        }

        #endregion
    }
}