namespace MetaMind.Engine.Core.Entity.Control.Item.Visuals
{
    using System;
    using Labels;
    using Microsoft.Xna.Framework;

    public class ViewItemLabel : Label
    {
        #region Constructors

        public ViewItemLabel(IMMViewItem item) 
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            this.Item = item;
        }

        #endregion

        #region Dependency

        public IMMViewItem Item { get; private set; }

        #endregion

        #region Draw

        public override void Draw(GameTime time)
        {
            if (MMViewItemState.Item_Is_Active.Match(this.Item))
            {
                return;
            }

            base.Draw(time);
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
                        this.Item = null;
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

    }
}