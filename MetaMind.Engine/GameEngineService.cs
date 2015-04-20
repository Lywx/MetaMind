// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameEngineService.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine
{
    using System;

    public class GameEngineService : IGameService
    {
        private static IGameAudio gameAudio;

        private static IGameGraphics gameGraphics;

        private static IGameInput gameInput;

        private static IGameInterop gameInterop;

        private static IGameNumerical gameNumerical;

        static GameEngineService()
        {
            gameNumerical = new GameEngineNumerical();
        }

        public IGameAudio GameAudio
        {
            get
            {
                return gameAudio;
            }
        }

        public IGameGraphics GameGraphics
        {
            get
            {
                return gameGraphics;
            }
        }

        public IGameInput GameInput
        {
            get
            {
                return gameInput;
            }
        }

        public IGameInterop GameInterop
        {
            get
            {
                return gameInterop;
            }
        }

        public IGameNumerical GameNumerical
        {
            get
            {
                return gameNumerical;
            }
        }

        public void Provide(IGameGraphics gameGraphics)
        {
            if (gameGraphics == null)
            {
                throw new ArgumentNullException("gameGraphics");
            }

            GameEngineService.gameGraphics = gameGraphics;
        }

        public void Provide(IGameAudio gameAudio)
        {
            if (gameAudio == null)
            {
                throw new ArgumentNullException("gameAudio");
            }

            GameEngineService.gameAudio = gameAudio;
        }

        public void Provide(IGameInput gameInput)
        {
            if (gameInput == null)
            {
                throw new ArgumentNullException("gameInput");
            }

            GameEngineService.gameInput = gameInput;
        }

        public void Provide(IGameInterop gameInterop)
        {
            if (gameInterop == null)
            {
                throw new ArgumentNullException("gameInterop");
            }

            GameEngineService.gameInterop = gameInterop;
        }
    }
}