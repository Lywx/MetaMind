namespace MetaMind.Engine
{
    using System;

    using Microsoft.Xna.Framework;

    public interface IDrawable
    {
        event EventHandler<EventArgs> DrawOrderChanged;

        event EventHandler<EventArgs> VisibleChanged;

        int DrawOrder { get; }

        bool Visible { get; }

        void Draw(IGameGraphics gameGraphics, GameTime gameTime, byte alpha);
    }
}