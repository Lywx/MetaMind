namespace MetaMind.Engine.Components.Content.Fonts
{
    using System;
    using Microsoft.Xna.Framework;

    public interface IFontManager : IGameComponent, IDisposable
    {
        #region Font Access

        Font this[string index] { get; }

        #endregion

        #region Font Loading

        /// <summary>
        /// This method should be called from asset manager.
        /// </summary>
        /// <param name="fontAsset"></param>
        void Add(FontAsset fontAsset);

        #endregion

        void Remove(FontAsset fontAsset);
    }
}