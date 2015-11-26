namespace MetaMind.Session.Guis.Widgets.BlockViews.Options
{
    using Concepts.Operations;
    using Engine.Entities.Controls.Item;
    using Engine.Entities.Controls.Item.Controllers;
    using Engine.Entities.Controls.Item.Data;
    using Engine.Entities.Controls.Item.Frames;
    using Engine.Entities.Controls.Item.Interactions;
    using Engine.Entities.Controls.Item.Layouts;
    using Microsoft.Xna.Framework;
    using Screens;

    public class OptionItemLogic : MMBlockViewVerticalItemController
    {
        public OptionItemLogic(
            IMMViewItem            item,
            IMMViewItemFrameController       itemFrame,
            IMMViewItemInteraction itemInteraction,
            IMMViewItemDataModel   itemModel,
            IMMViewItemLayout      itemLayout)
            : base(item, itemFrame, itemInteraction, itemModel, itemLayout)
        {
        }

        public new OptionItemFrameController ItemFrame
        {
            get { return (OptionItemFrameController)base.ItemFrame; }
        }

        public override void Initialize()
        {
            base.Initialize();

            this.ItemFrame.RootImmRectangle.MouseUpLeft += (o, args) => this.AcceptOption();
        }

        private void AcceptOption()
        {
            IOption option = this.Item.ItemData;
            option.Accept();
            option.Unlock();

            this.ItemFrame.RootImmRectangle.UpdateInput(new GameTime());

            this.Interop.Screen.Screens.First(screen => screen is OptionScreen).Exit();
        }
    }
}
