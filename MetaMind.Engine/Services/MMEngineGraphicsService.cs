// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MMngineGraphicsService.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Services
{
    using Components;
    using Components.Graphics;
    using Microsoft.Xna.Framework.Graphics;

    public sealed class MMEngineGraphicsService : IMMEngineGraphicsService
    {
        public MMEngineGraphicsService(IMMEngineGraphics graphics)
        {
            this.Graphics = graphics;
        }

        public IMMEngineGraphics Graphics { get; }

        public MMGraphicsManager Manager => this.Graphics.Manager;

        public MMGraphicsSettings Settings => this.Graphics.Settings;

        public SpriteBatch SpriteBatch => this.Graphics.SpriteBatch;

        public IMMRenderer Renderer => this.Graphics.Renderer;

        public GraphicsDevice GraphicsDevice => this.Manager.GraphicsDevice;
    }
}