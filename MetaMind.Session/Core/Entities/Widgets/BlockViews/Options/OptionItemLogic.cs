namespace MetaMind.Session.Guis.Widgets.BlockViews.Options
{
    using Engine.Core.Entity.Control.Item;
    using Engine.Core.Entity.Control.Item.Controllers;
    using Engine.Core.Entity.Control.Item.Data;
    using Engine.Core.Entity.Control.Item.Frames;
    using Engine.Core.Entity.Control.Item.Interactions;
    using Engine.Core.Entity.Control.Item.Layouts;
    using Microsoft.Xna.Framework;
    using Operations;
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

            this.GlobalInterop.Screen.Screens.First(screen => screen is OptionScreen).Exit();
        }
    }
}
