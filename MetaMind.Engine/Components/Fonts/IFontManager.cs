namespace MetaMind.Engine.Components.Fonts
{
    using System.Collections.Generic;

    using Microsoft.Xna.Framework;

    public interface IFontManager : IGameComponent
    {
        Dictionary<Font, FontInfo> Fonts { get; }
    }
}