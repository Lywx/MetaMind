// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameEngineGraphics.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine
{
    using MetaMind.Engine.Components;
    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Components.Graphics;

    public sealed class GameEngineGraphics : GameEngineAccess, IGameGraphics
    {
        public GameEngineGraphics(GameEngine gameEngine)
            : base(gameEngine)
        {
            this.AccessType = GameEngineAccessType.Graphics;
        }

        public GraphicsManager Graphics
        {
            get
            {
                return GameEngine.GraphicsManager;
            }
        }

        public GraphicsSettings Settings
        {
            get
            {
                return GameEngine.GraphicsSettings;
            }
        }

        public IScreenManager Screen
        {
            get
            {
                return GameEngine.Screen;
            }
        }

        public MessageDrawer MessageDrawer
        {
            get
            {
                return GameEngine.MessageDrawer;
            }
        }

        public IStringDrawer StringDrawer
        {
            get
            {
                return GameEngine.StringDrawer;
            }
        }
    }
}