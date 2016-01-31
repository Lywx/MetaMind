namespace MetaMind.Engine.Core.Services
{
    using Backend;
    using Backend.Content.Asset;
    using Backend.Interop;
    using Backend.Interop.Event;
    using Backend.Interop.Process;
    using Backend.IO;
    using Console;
    using Microsoft.Xna.Framework.Content;

    public interface IMMEngineInteropService
    {
        IMMAssetManager Asset { get; }

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