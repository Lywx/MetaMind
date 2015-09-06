namespace MetaMind.Engine.Guis.Controls
{
    using System;
    using Microsoft.Xna.Framework.Graphics;

    public class Container : GameControllableEntity, IContainer
    {
        public Container(GraphicsDevice graphicsDevice)
        {
            if (graphicsDevice == null)
            {
                throw new ArgumentNullException(nameof(graphicsDevice));
            }

            this.GraphicsDevice = graphicsDevice;
        }

        protected GraphicsDevice GraphicsDevice { get; }
    }
}