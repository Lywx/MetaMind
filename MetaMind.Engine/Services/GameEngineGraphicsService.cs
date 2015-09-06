// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameEngineGraphicsService.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Services
{
    using Components;
    using Components.Fonts;
    using Components.Graphics;

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

        public IStringDrawer StringDrawer => this.Graphics.StringDrawer;

        public GraphicsDevice GraphicsDevice => this.Manager.GraphicsDevice;
    }
}