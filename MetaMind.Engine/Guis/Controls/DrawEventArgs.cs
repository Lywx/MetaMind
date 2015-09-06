namespace MetaMind.Engine.Guis.Controls
{
    using System;
    using Microsoft.Xna.Framework;

    public class DrawEventArgs : EventArgs
    {
        #region Constructors

        public DrawEventArgs(Rectangle rectangle, GameTime time)
        {
            this.Rectangle = rectangle;
            this.Time      = time;
        }

        #endregion

        public Rectangle Rectangle { get; }

        public GameTime Time { get; }
    }
}