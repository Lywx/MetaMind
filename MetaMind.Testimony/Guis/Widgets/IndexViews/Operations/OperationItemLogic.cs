namespace MetaMind.Testimony.Guis.Widgets.IndexViews.Operations
{
    using Concepts.Operations;
    using Engine.Guis.Widgets.Items;
    using Engine.Guis.Widgets.Items.Data;
    using Engine.Guis.Widgets.Items.Frames;
    using Engine.Guis.Widgets.Items.Interactions;
    using Engine.Guis.Widgets.Items.Layouts;
    using Engine.Guis.Widgets.Items.Logic;

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

            this.ItemFrame.NameFrame       .MouseLeftPressed  += (o, args) => this.ToggleIndexView();
            this.ItemFrame.DescriptionFrame.MouseLeftPressed  += (o, args) => this.ToggleIndexView();
            this.ItemFrame.StatusFrame     .MouseLeftPressed  += (o, args) => this.ToggleOperation();
            this.ItemFrame.RootFrame       .MouseRightPressed += (o, args) => this.OpenFolderPath();
        }

        #region Operations

        private void ToggleOperation()
        {
            ((IOperationDescription)this.Item.ItemData).Toggle();
        }

        #endregion
    }
}