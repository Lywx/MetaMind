// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameEngineAccess.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine
{
    /// <summary>
    /// Provide engine-game interface separation.
    /// </summary>
    public class GameEngineAccess
    {
        public GameEngineAccess()
        {
            this.AccessType = GameEngineAccessType.None;
        }

        protected GameEngineAccessType AccessType { get; set; }
    }
}