namespace MetaMind.Engine.Service
{
    using Component;
    using Component.Audio;
    using Component.Event;
    using Component.File;
    using Component.Graphics;
    using Component.Process;
    using Console;
    using Microsoft.Xna.Framework.Content;

    public interface IGameInteropService
    {
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