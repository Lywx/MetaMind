namespace MetaMind.Unity.Guis.Widgets.IndexViews.Operations
{
    using Concepts.Operations;
    using Engine.Gui.Controls.Item;
    using Engine.Gui.Controls.Item.Data;
    using Engine.Gui.Controls.Item.Frames;
    using Engine.Gui.Controls.Item.Interactions;
    using Engine.Gui.Controls.Item.Layouts;
    using Engine.Gui.Controls.Item.Logic;

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