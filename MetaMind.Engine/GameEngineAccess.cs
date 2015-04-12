// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameEngineAccess.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine
{
    /// <summary>
    ///     Provide engine-game interface separation.
    /// </summary>
    public class GameEngineAccess
    {
        protected GameEngineAccess(GameEngine gameEngine)
        {
            this.GameEngine = gameEngine;

            this.AccessType = GameEngineAccessType.None;
        }

        protected GameEngine GameEngine { get; private set; }

        protected GameEngineAccessType AccessType { get; set; }
    }
}