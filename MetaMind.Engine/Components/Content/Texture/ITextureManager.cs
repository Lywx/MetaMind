namespace MetaMind.Engine.Components.Content.Texture
{
    using Microsoft.Xna.Framework;

    public interface ITextureManager : IGameComponent
    {
        Image this[string index] { get; }

        void Add(ImageAsset imageAsset);

        void Remove(ImageAsset imageAsset);
    }
}