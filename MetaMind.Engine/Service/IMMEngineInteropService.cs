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

    public interface IMMEngineInteropService
    {
        IAssetManager Asset { get; }

        IAudioManager Audio { get; }

        ContentManager Content { get; }

        MMConsole Console { get; set; }

        IFileManager File { get; }

        IEventManager Event { get; }

        IMMGameManager Game { get; }

        MMEngine Engine { get; }

        IProcessManager Process { get; }

        IMMScreenDirector Screen { get; }

        /// <remarks>
        /// Save that is replaceable in specific game 
        /// </remarks>
        IMMSaveManager Save { get; set; }
    }
}