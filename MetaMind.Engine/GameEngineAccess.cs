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

            askdljasl
            this.GameEngine.Services.AddService(new object());

            this.GameEngine.Services.GetService<object>();
        }

        protected enum GameEngineAccessType
        {
            None,

            File,

            Sound,

            Graphics,

            Input,

            Interop,
        }

        protected GameEngineAccessType AccessType { get; set; }

        protected GameEngine GameEngine { get; private set; }
    }
}