namespace MetaMind.Engine
{
    using MetaMind.Engine.Components;

    using Microsoft.Xna.Framework.Content;

    public interface IGameFile
    {
        ContentManager Content { get; }

        FolderManager Folder { get; }
    }
}