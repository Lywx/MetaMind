namespace MetaMind.Engine.Core.Backend.Content.Asset
{
    using System;
    using Fonts;
    using Microsoft.Xna.Framework;
    using Texture;

    public interface IMMAssetManager : IGameComponent, IDisposable
    {
        #region Content

        IMMFontManager Fonts { get; }

        IMMTextureManager Texture { get; }

        #endregion

        #region Operations

        void LoadPackage(string packageName, bool async = false);

        void UnloadPackage(string packageName);

        #endregion
    }
}