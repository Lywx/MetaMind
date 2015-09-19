namespace MetaMind.Engine.Component.Font
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;

    public interface IFontManager : IGameComponent, IDisposable
    {
        Dictionary<Font, FontInfo> Fonts { get; }
    }
}