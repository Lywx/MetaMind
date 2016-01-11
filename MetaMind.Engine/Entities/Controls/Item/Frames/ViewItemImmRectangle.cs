namespace MetaMind.Engine.Entities.Controls.Item.Frames
{
    using System;
    using Entities.Elements;
    using Entities.Elements.Rectangles;

    public class ViewItemImmRectangle : MMDraggableRectangleElement
    {
        #region Constructors and Finalizer

        public ViewItemImmRectangle(IMMViewItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            this.Item = item;

            this.RegisterHandlers();
        }

        ~ViewItemImmRectangle()
        {
            this.Dispose(true);
        }


        #endregion

        #region Dependency

        private IMMViewItem Item { get; set; }

        private IMMViewItemController ItemLogic => this.Item.ItemLogic;

        #endregion

        #region Initialization

        private void RegisterHandlers()
        {
            this.MousePressLeft += this.ViewSelect;
            this.MousePressOutLeft += this.ViewUnselect;
        }

        #endregion

        #region Event Handlers

        private void ViewSelect(object sender, MMInputElementDebugEventArgs e)
        {
            this.ItemLogic.ItemInteraction.ViewSelect();
        }

        private void ViewUnselect(object sender, MMInputElementDebugEventArgs e)
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