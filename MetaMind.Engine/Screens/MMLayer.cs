namespace MetaMind.Engine.Screens
{
    using System;
    using Entities.Nodes;

    public class MMLayer : MMNode, IMMLayer
    {
        #region Constructors and Finalizer

        public MMLayer(IMMScreen screen)
        {
            if (screen == null)
            {
                throw new ArgumentNullException(nameof(screen));
            }

            this.Screen = screen;
        }

        ~MMLayer()
        {
            this.Dispose(true);
        }

        #endregion

        #region Render Data

        public IMMScreen Screen { get; private set; }

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