namespace MetaMind.Unity.Guis.Widgets.BlockViews.Options
{
    using System.Linq;
    using Concepts.Operations;
    using Engine.Gui.Control.Item;
    using Engine.Gui.Control.Item.Data;
    using Engine.Gui.Control.Item.Frames;
    using Engine.Gui.Control.Item.Interactions;
    using Engine.Gui.Control.Item.Layouts;
    using Engine.Gui.Control.Item.Logic;
    using Microsoft.Xna.Framework;
    using Screens;

    public class OptionItemLogic : BlockViewVerticalItemLogic
    {
        public OptionItemLogic(
            IViewItem            item,
            IViewItemFrameController       itemFrame,
            IViewItemInteraction itemInteraction,
            IViewItemDataModel   itemModel,
            IViewItemLayout      itemLayout)
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

            this.ItemFrame.RootRectangle.MouseUpLeft += (o, args) => this.AcceptOption();
        }

        private void AcceptOption()
        {
            IOption option = this.Item.ItemData;
            option.Accept();
            option.Unlock();

            this.ItemFrame.RootRectangle.UpdateInput(this.Input, new GameTime());

            this.Interop.Screen.Screens.First(screen => screen is OptionScreen).Exit();
        }
    }
}
