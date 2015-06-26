namespace MetaMind.Testimony.Guis.Widgets.IndexViews.Operations
{
    using System.Diagnostics;
    using Engine.Guis.Elements;
    using Engine.Guis.Widgets.Items;
    using Engine.Guis.Widgets.Items.Data;
    using Engine.Guis.Widgets.Items.Frames;
    using Engine.Guis.Widgets.Items.Interactions;
    using Engine.Guis.Widgets.Items.Layouts;
    using Engine.Guis.Widgets.Items.Logic;

    public class OperationItemLogic : IndexBlockViewVerticalItemLogic 
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

        public new StandardItemFrame ItemFrame
        {
            get { return (StandardItemFrame)base.ItemFrame; }
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
            this.OpenOperationFile();
        }

        #endregion

        #region Operations

        private void OpenOperationFile()
        {
            Process.Start(this.Item.ItemData.Path);
        }

        #endregion
    }
}