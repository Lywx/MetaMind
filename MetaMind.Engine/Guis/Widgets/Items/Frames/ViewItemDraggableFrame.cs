namespace MetaMind.Engine.Guis.Widgets.Items.Frames
{
    using Elements;
    using Logic;

    public class ViewItemDraggableFrame : DraggableFrame, IViewItemRootFrame
    {
        public ViewItemDraggableFrame(IViewItem item)
        {
            this.Item = item;

            this.MouseLeftPressed        += this.ViewSelect;
            this.MouseLeftPressedOutside += this.ViewUnselect;
        }

        ~ViewItemDraggableFrame()
        {
            this.Dispose();
        }

        #region Direct Dependency

        private IViewItem Item { get; set; }

        #endregion

        #region Indirect Dependency

        private IViewItemLogic ItemLogic => this.Item.ItemLogic;

        #endregion

        #region IDisposable

        private bool IsDisposed { get; set; }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    if (!this.IsDisposed)
                    {
                        this.MouseLeftPressed        -= this.ViewSelect;
                        this.MouseLeftPressedOutside -= this.ViewUnselect;
                    }
                }
            }
            catch
            {
                // Ignored
            }
            finally
            {
                base.Dispose(disposing);
            }
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