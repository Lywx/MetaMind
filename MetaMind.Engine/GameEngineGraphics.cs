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

        public IScreenManager Screens
        {
            get
            {
                return GameEngine.Screens;
            }
        }

        public MessageManager Message
        {
            get
            {
                return GameEngine.Message;
            }
        }

        public IFontDrawer FontDrawer
        {
            get
            {
                return GameEngine.FontManager;
            }
        }
    }
}