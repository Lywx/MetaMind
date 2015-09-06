namespace MetaMind.Unity.Guis.Widgets.BlockViews.Options
{
    using System.Linq;
    using Concepts.Operations;
    using Engine.Guis.Controls.Items;
    using Engine.Guis.Controls.Items.Data;
    using Engine.Guis.Controls.Items.Frames;
    using Engine.Guis.Controls.Items.Interactions;
    using Engine.Guis.Controls.Items.Layouts;
    using Engine.Guis.Controls.Items.Logic;
    using Microsoft.Xna.Framework;
    using Screens;

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

        public override void SetupLayer()
        {
            base.SetupLayer();

            this.ItemFrame.RootFrame.MouseLeftReleased += (o, args) => this.AcceptOption();
        }

        private void AcceptOption()
        {
            IOption option = this.Item.ItemData;
            option.Accept();
            option.Unlock();

            this.ItemFrame.RootFrame.UpdateInput(this.Input, new GameTime());

            this.Interop.Screen.Screens.First(screen => screen is OptionScreen).Exit();
        }
    }
}
