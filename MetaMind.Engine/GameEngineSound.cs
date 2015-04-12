// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameEngineSound.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine
{
    using MetaMind.Engine.Components;

    public sealed class GameEngineSound : GameEngineAccess, IGameSound
    {
        public GameEngineSound(GameEngine gameEngine)
            : base(gameEngine)
        {
            this.AccessType = GameEngineAccessType.Sound;
        }

        public AudioManager Audio
        {
            get
            {
                return GameEngine.AudioManager;
            }
        }
    }
}