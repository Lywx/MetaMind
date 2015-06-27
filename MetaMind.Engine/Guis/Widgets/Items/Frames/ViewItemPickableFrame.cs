namespace MetaMind.Engine.Guis.Widgets.Items.Frames
{
    using Elements;
    using Logic;

    public class ViewItemPickableFrame : PickableFrame, IViewItemRootFrame
    {
        public ViewItemPickableFrame(IViewItem item)
        {
            this.Item = item;

            this.MouseLeftPressed        += this.ViewSelect;
            this.MouseLeftPressedOutside += this.ViewUnselect;
        }

        ~ViewItemPickableFrame()
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
            this.ItemLogic.ItemInteraction.ViewSelect();
        }

        private void ViewUnselect(object sender, FrameEventArgs e)
        {
            this.ItemLogic.ItemInteraction.ViewUnselect();
        }

        #endregion
    }
}