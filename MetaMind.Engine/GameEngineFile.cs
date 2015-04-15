// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameEngineFile.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine
{
    using MetaMind.Engine.Components;

    using Microsoft.Xna.Framework.Content;

    public sealed class GameEngineFile : GameEngineAccess, IGameFile
    {
        public GameEngineFile(GameEngine gameEngine)
            : base(gameEngine)
        {
            this.AccessType = GameEngineAccessType.File;
        }

        public ContentManager Content
        {
            get
            {
                return GameEngine.Content;
            }
        }

        public FolderManager Folder
        {
            get
            {
                return GameEngine.Folder;
            }
        }
    }
}