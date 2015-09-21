// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameEngineGraphicsService.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Service
{
    using Component;
    using Component.Graphics;
    using Microsoft.Xna.Framework.Graphics;

    public sealed class GameEngineGraphicsService : IGameGraphicsService
    {
        public GameEngineGraphicsService(IGameGraphics graphics)
        {
            this.Graphics = graphics;
        }

        public IGameGraphics Graphics { get; }

        public GraphicsManager Manager => this.Graphics.Manager;

        public GraphicsSettings Settings => this.Graphics.Settings;

        public SpriteBatch SpriteBatch => this.Graphics.SpriteBatch;

        public IRenderer Renderer => this.Graphics.Renderer;

        public GraphicsDevice GraphicsDevice => this.Manager.GraphicsDevice;
    }
}