// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameEngineGraphics.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine
{
    using MetaMind.Engine.Components;
    using MetaMind.Engine.Components.Graphics;

    public sealed class GameEngineGraphics : GameEngineAccess, IGameGraphics
    {
        public GameEngineGraphics()
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

        public ScreenManager Screen
        {
            get
            {
                return GameEngine.ScreenManager;
            }
        }

        public MessageManager Message
        {
            get
            {
                return GameEngine.MessageManager;
            }
        }

        public FontManager Font
        {
            get
            {
                return GameEngine.FontManager;
            }
        }
    }
}