namespace MetaMind.Engine.Service
{
    using Component;
    using Component.Audio;
    using Component.Content;
    using Component.Content.Asset;
    using Component.File;
    using Component.Graphics;
    using Component.Interop;
    using Component.Interop.Event;
    using Component.Interop.Process;
    using Console;
    using Microsoft.Xna.Framework.Content;

    public interface IGameInteropService
    {
        IAssetManager Asset { get; }

        IAudioManager Audio { get; }

        ContentManager Content { get; }

        GameConsole Console { get; set; }

        IFileManager File { get; }

        IEventManager Event { get; }

        IGameManager Game { get; }

        GameEngine Engine { get; }

        IProcessManager Process { get; }

        IScreenManager Screen { get; }

        /// <remarks>
        /// Save that is replaceable in specific game 
        /// </remarks>
        ISaveManager Save { get; set; }
    }
}