namespace MetaMind.Engine.Core.Backend.Content.Texture
{
    using Microsoft.Xna.Framework;

    public interface IMMTextureManager : IGameComponent
    {
        MMImage this[string index] { get; }

        void Add(MMImageAsset imageAsset);

        void Remove(MMImageAsset imageAsset);
    }
}