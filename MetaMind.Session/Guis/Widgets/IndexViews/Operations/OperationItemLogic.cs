namespace MetaMind.Session.Guis.Widgets.IndexViews.Operations
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

            this.ItemFrame.NameImmRectangle       .MousePressLeft  += (o, args) => this.ToggleIndexView();
            this.ItemFrame.DescriptionImmRectangle.MousePressLeft  += (o, args) => this.ToggleIndexView();
            this.ItemFrame.StatusImmRectangle     .MousePressLeft  += (o, args) => this.ToggleOperation();
            this.ItemFrame.RootImmRectangle       .MousePressRight += (o, args) => this.SelectPath();
        }

        #region Operations

        private void ToggleOperation()
        {
            ((IOperationDescription)this.Item.ItemData).Toggle();
        }

        #endregion
    }
}