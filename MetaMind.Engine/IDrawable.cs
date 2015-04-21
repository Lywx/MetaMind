namespace MetaMind.Engine
{
    using System;

    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public interface IDrawable
    {
        event EventHandler<EventArgs> DrawOrderChanged;

        event EventHandler<EventArgs> VisibleChanged;

        int DrawOrder { get; }

        bool Visible { get; }

        void Draw(IGameGraphicsService graphics, GameTime time, byte alpha);
    }
}