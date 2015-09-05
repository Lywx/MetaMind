namespace MetaMind.Engine
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public interface IDrawableGameComponent : IGameComponent, IDrawableComponentOperations
    {
        GraphicsDevice GraphicsDevice { get; }

        event EventHandler<EventArgs> DrawOrderChanged;

        event EventHandler<EventArgs> VisibleChanged;

        int DrawOrder { get; }

        bool Visible { get; }
    }
}