namespace MetaMind.Engine.Services
{
    using Components;
    using Components.Audio;
    using Components.Content.Asset;
    using Components.Interop;
    using Components.Interop.Event;
    using Components.Interop.Process;
    using Components.IO;
    using Console;
    using Microsoft.Xna.Framework.Content;

    public interface IMMEngineInteropService
    {
        IAssetManager Asset { get; }

        IMMAudioManager Audio { get; }

        ContentManager Content { get; }

        MMConsole Console { get; set; }

        IMMDirectoryManager File { get; }

        IMMEventManager Event { get; }

        IMMGameManager Game { get; }

        MMEngine Engine { get; }

        IMMProcessManager Process { get; }

        IMMScreenDirector Screen { get; }

        /// <remarks>
        /// Save that is replaceable in specific game 
        /// </remarks>
        IMMSaveManager Save { get; set; }
    }
}