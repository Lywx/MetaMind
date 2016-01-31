namespace MetaMind.Session.Guis.Widgets.IndexViews.Operations
{
    using Engine.Core.Entity.Control.Item;
    using Engine.Core.Entity.Control.Item.Controllers;
    using Engine.Core.Entity.Control.Item.Data;
    using Engine.Core.Entity.Control.Item.Frames;
    using Engine.Core.Entity.Control.Item.Interactions;
    using Engine.Core.Entity.Control.Item.Layouts;
    using Session.Operations;

    public class OperationItemLogic : MMIndexBlockViewVerticalItemLogic 
    {
        public OperationItemLogic(
            IMMViewItem            item,
            IMMViewItemFrameController       itemFrame,
            IMMViewItemInteraction itemInteraction,
            IMMViewItemDataModel   itemModel,
            IMMViewItemLayout      itemLayout)
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