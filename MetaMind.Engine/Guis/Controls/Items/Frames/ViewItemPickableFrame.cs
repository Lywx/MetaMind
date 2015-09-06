namespace MetaMind.Engine.Guis.Controls.Items.Frames
{
    using Elements;
    using Logic;

    public class ViewItemPickableFrame : PickableFrame, IViewItemRootFrame
    {
        public ViewItemPickableFrame(IViewItem item)
        {
            this.Item = item;

            this.MousePressLeft += this.ViewSelect;
            this.MousePressOutLeft += this.ViewUnselect;
        }

        ~ViewItemPickableFrame()
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
                        this.MousePressLeft -= this.ViewSelect;
                        this.MousePressOutLeft -= this.ViewUnselect;
                    }

                    this.IsDisposed = true;
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