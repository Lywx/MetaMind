// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameEngineGraphicsAccess.cs" company="UESTC">
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

    public sealed class GameEngineGraphicsAccess : GameEngineAccess, IGameGraphicsService
    {
        public GameEngineGraphicsAccess(GameEngine engine)
            : base(engine)
        {
        }

        public GraphicsManager Manager
        {
            get
            {
                return this.Engine.GraphicsManager;
            }
        }

        public GraphicsSettings Settings
        {
            get
            {
                return this.Engine.GraphicsSettings;
            }
        }

        public SpriteBatch SpriteBatch { get{return }}

        public IScreenManager Screen
        {
            get
            {
                return this.Engine.Screen;
            }
        }

        public IStringDrawer String
        {
            get
            {
                return this.Engine.StringDrawer;
            }
        }
    }
}