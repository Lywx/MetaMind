namespace MetaMind.Testimony.Guis.Widgets.IndexViews.Tests
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
            TestItemFrame    itemFrame,
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

            this.ItemFrame.RootFrame.MouseLeftPressed  += this.RootFrameMouseLeftPressed;
            this.ItemFrame.RootFrame.MouseRightPressed += this.RootFrameMouseRightPressed;
        }

        #region Events

        private void RootFrameMouseLeftPressed(object sender, FrameEventArgs e)
        {
            this.ToggleIndexView();
        }

        private void RootFrameMouseRightPressed(object sender, FrameEventArgs e)
        {
            this.OpenTestFile();
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