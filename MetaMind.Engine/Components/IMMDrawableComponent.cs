namespace MetaMind.Engine.Components
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public interface IMMDrawableComponent : IGameComponent, IMMDrawableComponentOperations
    {
        GraphicsDevice GraphicsDevice { get; }

        event EventHandler<EventArgs> DrawOrderChanged;

        event EventHandler<EventArgs> VisibleChanged;

        int DrawOrder { get; }

        bool Visible { get; }
    }
}