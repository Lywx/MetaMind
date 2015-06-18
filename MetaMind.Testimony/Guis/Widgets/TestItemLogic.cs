namespace MetaMind.Testimony.Guis.Widgets
{
    using System.Diagnostics;
    using Engine.Guis.Elements;
    using Engine.Guis.Widgets.Items;
    using Engine.Guis.Widgets.Items.Data;
    using Engine.Guis.Widgets.Items.Interactions;
    using Engine.Guis.Widgets.Items.Layouts;
    using Engine.Guis.Widgets.Items.Logic;

    public class TestItemLogic : IndexBlockViewVerticalItemLogic
    {
        public TestItemLogic(
            IViewItem            item,
            TestItemFrame        itemFrame,
            IViewItemInteraction itemInteraction,
            IViewItemDataModel   itemModel,
            IViewItemLayout      itemLayout)
            : base(item, itemFrame, itemInteraction, itemModel, itemLayout)
        {
        }

        public new TestItemFrame ItemFrame
        {
            get { return (TestItemFrame)base.ItemFrame; }
        }

        public override void SetupLayer()
        {
            base.SetupLayer();

            this.ItemFrame.RootFrame.MouseLeftDoubleClicked += this.RootFrameMouseLeftDoubleClicked;
            this.ItemFrame.RootFrame.MouseRightDoubleClicked += this.RootFrameMouseRightDoubleClicked;
        }

        #region Events

        private void RootFrameMouseRightDoubleClicked(object sender, FrameEventArgs e)
        {
            this.OpenTestFile();
        }

        private void RootFrameMouseLeftDoubleClicked(object sender, FrameEventArgs e)
        {
            this.ToggleIndexView();
        }

        #endregion

        #region Operations

        private void OpenTestFile()
        {
            Process.Start(this.Item.ItemData.Path);
        }

        #endregion
    }
}