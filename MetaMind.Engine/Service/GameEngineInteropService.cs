// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameEngineInteropService.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Service
{
    using Component;
    using Component.Audio;
    using Component.File;
    using Component.Graphics;
    using Component.Interop;
    using Component.Interop.Event;
    using Component.Interop.Process;
    using Console;
    using Microsoft.Xna.Framework.Content;

    /// <remarks>
    ///     Sealed for added new keyword and changed accessibility in GameEngine property
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

        public IFileManager File
        {
            get
            {
                return this.interop.File;
            }
        }

        public GameEngine Engine
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

            set
            {
                this.interop.Save = value;
            }
        }
    }
}