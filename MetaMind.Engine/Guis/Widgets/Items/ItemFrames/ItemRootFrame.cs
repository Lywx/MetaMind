namespace MetaMind.Engine.Guis.Widgets.Items.ItemFrames
{
    using MetaMind.Engine.Guis.Elements;
    using MetaMind.Engine.Guis.Widgets.Items.ItemView;

    public class ItemRootFrame : DraggableFrame, IItemRootFrame
    {
        public ItemRootFrame(IViewItem item)
        {
            this.Item = item;

            this.MouseLeftPressed        += this.ViewDoSelect;
            this.MouseLeftPressedOutside += this.ViewDoUnselect;
        }

        ~ItemRootFrame()
        {
            this.Dispose();
        }

        #region Direct Dependency

        private IViewItem Item { get; set; }

        #endregion

        #region Indirect Dependency

        private IViewItemLogic ItemLogic
        {
            get
            {
                return this.Item.ItemLogic;
            }
        }

        #endregion

        #region IDisposable

        public override void Dispose()
        {
            this.MouseLeftPressed -= this.ViewDoSelect;
            this.MouseLeftPressedOutside -= this.ViewDoUnselect;

            base.Dispose();
        }

        #endregion

        #region Events

        private void ViewDoSelect(object sender, FrameEventArgs e)
        {
            var viewContent = this.ItemLogic.ItemViewControl as IViewSelectionContent;
            if (viewContent != null)
            {
                viewContent.ViewDoSelect();
            }
        }

        private void ViewDoUnselect(object sender, FrameEventArgs e)
        {
            var viewContent = this.ItemLogic.ItemViewControl as IViewSelectionContent;
            if (viewContent != null)
            {
                viewContent.ViewDoUnselect();
            }
        }

        #endregion
    }
}