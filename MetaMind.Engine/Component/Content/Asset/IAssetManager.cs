namespace MetaMind.Engine.Component.Content.Asset
{
    using Fonts;
    using Microsoft.Xna.Framework;
    using Texture;

    public interface IAssetManager : IGameComponent
    {
        #region Content

        IFontManager Fonts { get; }

        ITextureManager Texture { get; }

        #endregion

        #region Operations

        void LoadPackage(string packageName, bool async = false);

        void UnloadPackage(string packageName);

        #endregion
    }
}