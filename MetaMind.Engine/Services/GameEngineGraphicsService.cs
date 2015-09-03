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
        private readonly IGameGraphics graphics;

        public GameEngineGraphicsService(IGameGraphics graphics)
        {
            this.graphics = graphics;
        }

        public GraphicsManager Manager => this.graphics.Manager;

        public GraphicsSettings Settings => this.graphics.Settings;

        public SpriteBatch SpriteBatch => this.graphics.SpriteBatch;

        public IStringDrawer StringDrawer => this.graphics.StringDrawer;
    }
}