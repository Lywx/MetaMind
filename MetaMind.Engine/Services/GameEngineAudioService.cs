// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameEngineAudioService.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Services
{
    using MetaMind.Engine.Components;

    public sealed class GameEngineAudioService : GameEngineAccess, IGameAudioService
    {
        public GameEngineAudioService(IGameAudio audio)
            : base(engine)
        {
        }

        public IAudioManager Audio
        {
            get
            {
                return this.Audio;
            }
        }
    }
}