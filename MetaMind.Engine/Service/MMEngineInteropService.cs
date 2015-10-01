// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MMEngineInteropService.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Service
{
    using Components;
    using Components.Audio;
    using Components.Content.Asset;
    using Components.File;
    using Components.Interop;
    using Components.Interop.Event;
    using Components.Interop.Process;
    using Console;
    using Microsoft.Xna.Framework.Content;

    /// <remarks>
    ///     Sealed for added new keyword and changed accessibility in MMEngine property
    /// </remarks>
    public sealed class MMEngineInteropService : IMMEngineInteropService
    {
        private readonly IMMEngineInterop interop;

        public MMEngineInteropService(IMMEngineInterop interop)
        {
            this.interop = interop;
        }

        public IAssetManager Asset => this.interop.Asset;

        public IAudioManager Audio => this.interop.Audio;

        public ContentManager Content => this.interop.Content;

        public MMConsole Console
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

        public IFileManager File => this.interop.File;

        public MMEngine Engine => this.interop.Engine;

        public IEventManager Event => this.interop.Event;

        public IMMGameManager Game => this.interop.Game;

        public IProcessManager Process => this.interop.Process;

        public IMMScreenDirector Screen => this.interop.Screen;

        public IMMSaveManager Save
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