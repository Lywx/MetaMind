namespace MetaMind.Testimony.Guis.Widgets.Options
{
    using Engine.Guis.Widgets.Items;
    using Engine.Guis.Widgets.Items.Data;
    using Engine.Guis.Widgets.Items.Frames;
    using Engine.Guis.Widgets.Items.Interactions;
    using Engine.Guis.Widgets.Items.Layouts;
    using Engine.Guis.Widgets.Items.Logic;

    public class OptionItemLogic : BlockViewVerticalItemLogic
    {
        public OptionItemLogic(
            IViewItem            item,
            IViewItemFrame       itemFrame,
            IViewItemInteraction itemInteraction,
            IViewItemDataModel   itemModel,
            IViewItemLayout      itemLayout)
            : base(item, itemFrame, itemInteraction, itemModel, itemLayout)
        {
        }

        public new OptionItemFrame ItemFrame
        {
            get { return (OptionItemFrame)base.ItemFrame; }
        }
    }
}
