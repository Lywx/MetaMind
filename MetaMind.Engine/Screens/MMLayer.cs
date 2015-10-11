namespace MetaMind.Engine.Screens
{
    using System;
    using Entities.Nodes;
    using Microsoft.Xna.Framework;

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

            this.Location = Point.Zero;
            this.Width    = this.Screen.Width;
            this.Height   = this.Screen.Height;
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
