namespace MetaMind.Engine.Services
{
    using MetaMind.Engine.Components;

    using Microsoft.Xna.Framework.Content;

    public interface IGameInteropService
    {
        IAudioManager Audio { get; }

        ContentManager Content { get; }

        FolderManager Folder { get; }

        IEventManager Event { get; }

        IGameManager Game { get; }

        IGameEngine Engine { get; }

        IProcessManager Process { get; }

        IScreenManager Screen { get; }
    }
}