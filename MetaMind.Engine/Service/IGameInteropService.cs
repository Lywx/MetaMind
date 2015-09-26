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