// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameEngineInteropService.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Services
{
    using MetaMind.Engine.Components;
    using MetaMind.Engine.Guis.Console;

    using Microsoft.Xna.Framework.Content;

    /// <remarks>
    ///     Sealed for added new keyword and changed acceesibility in GameEngine property
    /// </remarks>
    public sealed class GameEngineInteropService : IGameInteropService
    {
        private readonly IGameInterop interop;

        public GameEngineInteropService(IGameInterop interop)
        {
            this.interop = interop;
        }

        public IAudioManager Audio
        {
            get
            {
                return this.interop.Audio;
            }
        }

        public ContentManager Content
        {
            get
            {
                return this.interop.Content;
            }
        }

        public GameConsole Console
        {
            get
            {
                return this.interop.Console;
            }
            set
            {
                this.interop.Console = value;
            }
        }

        public FileManager File
        {
            get
            {
                return this.interop.File;
            }
        }

        public IGameEngine Engine
        {
            get
            {
                return this.interop.Engine;
            }
        }

        public IEventManager Event
        {
            get
            {
                return this.interop.Event;
            }
        }

        public IGameManager Game
        {
            get
            {
                return this.interop.Game;
            }
        }

        public IProcessManager Process
        {
            get
            {
                return this.interop.Process;
            }
        }

        public IScreenManager Screen
        {
            get
            {
                return this.interop.Screen;
            }
        }

        public ISaveManager Save
        {
            get
            {
                return this.interop.Save;
            }
        }
    }
}