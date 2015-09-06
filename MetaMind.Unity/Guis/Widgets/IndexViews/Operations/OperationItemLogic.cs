namespace MetaMind.Unity.Guis.Widgets.IndexViews.Operations
{
    using Concepts.Operations;
    using Engine.Guis.Controls.Items;
    using Engine.Guis.Controls.Items.Data;
    using Engine.Guis.Controls.Items.Frames;
    using Engine.Guis.Controls.Items.Interactions;
    using Engine.Guis.Controls.Items.Layouts;
    using Engine.Guis.Controls.Items.Logic;

    public class OperationItemLogic : IndexBlockViewVerticalItemLogic 
    {
        public OperationItemLogic(
            IViewItem            item,
            IViewItemFrame       itemFrame,
            IViewItemInteraction itemInteraction,
            IViewItemDataModel   itemModel,
            IViewItemLayout      itemLayout)
            : base(item, itemFrame, itemInteraction, itemModel, itemLayout)
        {
        }

        public new OperationItemFrame ItemFrame
        {
            get { return (OperationItemFrame)base.ItemFrame; }
        }

        public override void SetupLayer()
        {
            base.SetupLayer();

            this.ItemFrame.NameFrame       .MousePressLeft  += (o, args) => this.ToggleIndexView();
            this.ItemFrame.DescriptionFrame.MousePressLeft  += (o, args) => this.ToggleIndexView();
            this.ItemFrame.StatusFrame     .MousePressLeft  += (o, args) => this.ToggleOperation();
            this.ItemFrame.RootFrame       .MousePressRight += (o, args) => this.SelectPath();
        }

        #region Operations

        private void ToggleOperation()
        {
            ((IOperationDescription)this.Item.ItemData).Toggle();
        }

        #endregion
    }
}