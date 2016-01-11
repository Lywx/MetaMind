namespace MetaMind.Engine.Components.Content.Fonts
{
    using System;
    using Microsoft.Xna.Framework;

    public interface IMMFontManager : IGameComponent, IDisposable
    {
        #region Font Access

        MMFont this[string index] { get; }

        #endregion

        #region Font Loading

        /// <summary>
        /// This method should be called from asset manager.
        /// </summary>
        /// <param name="fontAsset"></param>
        void Add(MMFontAsset fontAsset);

        #endregion

        void Remove(MMFontAsset fontAsset);
    }
}