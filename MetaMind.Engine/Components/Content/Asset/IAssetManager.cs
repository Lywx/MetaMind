namespace MetaMind.Engine.Components.Content.Asset
{
    using System;
    using Fonts;
    using Microsoft.Xna.Framework;
    using Texture;

    public interface IAssetManager : IGameComponent, IDisposable
    {
        #region Content

        IFontManager Fonts { get; }

        IMMTextureManager Texture { get; }

        #endregion

        #region Operations

        void LoadPackage(string packageName, bool async = false);

        void UnloadPackage(string packageName);

        #endregion
    }
}