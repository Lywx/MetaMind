﻿namespace MetaMind.Engine.Components
{
    using Graphics;
    using Interop;
    using Microsoft.Xna.Framework.Graphics;

    internal class MMEngineNullGraphics : IMMEngineGraphics
    {
        public GraphicsManager Manager { get; }

        public IMMScreenDirector Screen { get; }

        public GraphicsSettings Settings { get; }

        public SpriteBatch SpriteBatch { get; }

        public IRenderer Renderer { get; }

        public GraphicsDevice GraphicsDevice { get; }

        public void Initialize()
        {
        }

        public void Dispose()
        {
        }
    }
}