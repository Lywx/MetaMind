namespace MetaMind.Testimony.Guis.Widgets.Operations
{
    using Engine.Guis.Widgets.Items;
    using Engine.Guis.Widgets.Items.Data;
    using Engine.Guis.Widgets.Items.Frames;
    using Engine.Guis.Widgets.Items.Interactions;
    using Engine.Guis.Widgets.Items.Layouts;
    using Engine.Guis.Widgets.Items.Logic;

    public class OperationItemLogic : BlockViewVerticalItemLogic 
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
    }
}
