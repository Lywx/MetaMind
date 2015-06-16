namespace MetaMind.Testimony.Guis.Widgets
{
    using Engine.Guis.Elements;
    using Engine.Guis.Widgets.Items;
    using Engine.Guis.Widgets.Items.Data;
    using Engine.Guis.Widgets.Items.Interactions;
    using Engine.Guis.Widgets.Items.Layouts;
    using Engine.Guis.Widgets.Items.Logic;
    using Engine.Guis.Widgets.Views;

    public class TestItemLogic : IndexBlockViewVerticalItemLogic
    {
        public TestItemLogic(
            IViewItem            item,
            TestItemFrame        itemFrame,
            IViewItemInteraction itemInteraction,
            IViewItemDataModel   itemModel,
            IViewItemLayout      itemLayout,
            IIndexViewComposer viewComposer)
            : base(item, itemFrame, itemInteraction, itemModel, itemLayout, viewComposer)
        {
        }

        public new TestItemFrame ItemFrame
        {
            get { return (TestItemFrame)base.ItemFrame; }
        }

        public override void SetupLayer()
        {
            base.SetupLayer();

            this.ItemFrame.PlusFrame.MouseLeftPressed += this.PlusFrameMouseLeftPressed;
        }

        #region Events

        private void PlusFrameMouseLeftPressed(object sender, FrameEventArgs e)
        {
            if (!this.IndexViewOpened)
            {
                this.OpenIndexView();
            }
            else
            {
                this.CloseIndexView();
            }
        }

        #endregion
    }
}