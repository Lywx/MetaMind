namespace MetaMind.Engine.Services
{
    using MetaMind.Engine.Components;
    using MetaMind.Engine.Guis.Consoles;

    using Microsoft.Xna.Framework.Content;

    public interface IGameInteropService
    {
        IAudioManager Audio { get; }

        ContentManager Content { get; }

        GameConsole Console { get; set; }

        FolderManager Folder { get; }

        IEventManager Event { get; }

        IGameManager Game { get; }

        IGameEngine Engine { get; }

        IProcessManager Process { get; }

        IScreenManager Screen { get; }
    }
}