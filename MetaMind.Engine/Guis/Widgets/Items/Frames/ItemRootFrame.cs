namespace MetaMind.Engine.Guis.Widgets.Items.Frames
{
    using MetaMind.Engine.Guis.Elements;
    using MetaMind.Engine.Guis.Widgets.Items.Logic;

    public class ItemRootFrame : DraggableFrame, IItemRootFrame
    {
        public ItemRootFrame(IViewItem item)
        {
            this.Item = item;

            this.MouseLeftPressed        += this.ViewSelect;
            this.MouseLeftPressedOutside += this.ViewUnselect;
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
            this.MouseLeftPressed        -= this.ViewSelect;
            this.MouseLeftPressedOutside -= this.ViewUnselect;

            base.Dispose();
        }

        #endregion

        #region Events

        private void ViewSelect(object sender, FrameEventArgs e)
        {
            this.ItemLogic.ItemView.ViewSelect();
        }

        private void ViewUnselect(object sender, FrameEventArgs e)
        {
            this.ItemLogic.ItemView.ViewUnselect();
        }

        #endregion
    }
}