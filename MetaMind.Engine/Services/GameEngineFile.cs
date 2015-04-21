// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameEngineFile.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Services
{
    using MetaMind.Engine.Components;

    using Microsoft.Xna.Framework.Content;

    public sealed class GameEngineFile : GameEngineAccess, IGameFile
    {
        public GameEngineFile(GameEngine engine)
            : base(engine)
        {
            this.AccessType = GameEngineAccessType.File;
        }

        public ContentManager Content
        {
            get
            {
                return this.Engine.Content;
            }
        }

        public FolderManager Folder
        {
            get
            {
                return this.Engine.Folder;
            }
        }
    }
}