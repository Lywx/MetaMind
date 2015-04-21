// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameEngineAccess.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Services
{
    /// <summary>
    ///     Provide engine-game interface separation.
    /// </summary>
    public class GameEngineAccess
    {
        protected GameEngineAccess(GameEngine engine)
        {
            this.Engine = engine;
        }
        
        protected GameEngine Engine { get; private set; }
    }
}