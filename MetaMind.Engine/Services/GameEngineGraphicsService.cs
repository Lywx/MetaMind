// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameEngineGraphicsService.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Services
{
    using MetaMind.Engine.Components;
    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Components.Graphics;

    using Microsoft.Xna.Framework.Graphics;

    public sealed class GameEngineGraphicsService : IGameGraphicsService
    {
        private readonly IGameGraphics graphics;

        public GameEngineGraphicsService(IGameGraphics graphics)
        {
            this.graphics = graphics;
        }

        public GraphicsManager Manager
        {
            get
            {
                return this.graphics.Manager;
            }
        }

        public GraphicsSettings Settings
        {
            get
            {
                return this.graphics.Settings;
            }
        }

        public SpriteBatch SpriteBatch
        {
            get
            {
                return this.graphics.SpriteBatch;
            }
        }
        
        public IStringDrawer StringDrawer
        {
            get
            {
                return this.graphics.StringDrawer;
            }
        }
    }
}