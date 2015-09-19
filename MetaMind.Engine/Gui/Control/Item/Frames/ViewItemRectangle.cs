namespace MetaMind.Engine.Gui.Control.Item.Frames
{
    using System;
    using Element;
    using Element.Rectangles;
    using Logic;

    public class ViewItemRectangle : DraggableRectangle
    {
        #region Constructors and Finalizer

        public ViewItemRectangle(IViewItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            this.Item = item;

            this.RegisterHandlers();
        }

        ~ViewItemRectangle()
        {
            this.Dispose(true);
        }


        #endregion

        #region Dependency

        private IViewItem Item { get; set; }

        private IViewItemLogic ItemLogic => this.Item.ItemLogic;

        #endregion

        #region Initialization

        private void RegisterHandlers()
        {
            this.MousePressLeft += this.ViewSelect;
            this.MousePressOutLeft += this.ViewUnselect;
        }

        #endregion

        #region Events

        private void ViewSelect(object sender, ElementEventArgs e)
        {
            this.ItemLogic.ItemInteraction.ViewSelect();
        }

        private void ViewUnselect(object sender, ElementEventArgs e)
        {
            this.ItemLogic.ItemInteraction.ViewUnselect();
        }

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
                        this.DisposeHandlers();
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

        private void DisposeHandlers()
        {
            this.MousePressLeft -= this.ViewSelect;
            this.MousePressOutLeft -= this.ViewUnselect;
        }

        #endregion
    }
}