namespace MetaMind.Unity.Guis.Widgets.BlockViews.Options
{
    using System.Linq;
    using Concepts.Operations;
    using Engine.Gui.Controls.Item;
    using Engine.Gui.Controls.Item.Data;
    using Engine.Gui.Controls.Item.Frames;
    using Engine.Gui.Controls.Item.Interactions;
    using Engine.Gui.Controls.Item.Layouts;
    using Engine.Gui.Controls.Item.Logic;
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
