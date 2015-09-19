namespace MetaMind.Engine
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class DrawEventArgs : EventArgs
    {
        #region Constructors

        public DrawEventArgs(RenderTarget2D renderTarget, Rectangle destinationRectangle, GameTime time)
        {
            if (renderTarget == null)
            {
                throw new ArgumentNullException(nameof(renderTarget));
            }

            if (time == null)
            {
                throw new ArgumentNullException(nameof(time));
            }

            this.RenderTarget = renderTarget;
            this.DestinationRectangle = destinationRectangle;

            this.Time = time;
        }

        #endregion

        public RenderTarget2D RenderTarget { get; }

        public Rectangle DestinationRectangle { get; }

        public GameTime Time { get; }
    }
}