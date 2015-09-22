namespace MetaMind.Engine.Component.Content.Texture
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public interface ITextureManager : IGameComponent
    {
        Texture2D this[string index] { get; }

        void Add(ImageAsset imageAsset);

        void Remove(ImageAsset imageAsset);
    }
}