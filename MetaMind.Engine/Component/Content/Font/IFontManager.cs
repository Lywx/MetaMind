namespace MetaMind.Engine.Component.Content.Font
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;

    public interface IFontManager : IGameComponent, IDisposable
    {
        Dictionary<string, FontAsset> Fonts { get; }
    }
}