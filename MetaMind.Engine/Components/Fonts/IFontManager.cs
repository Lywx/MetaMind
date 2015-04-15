namespace MetaMind.Engine.Components.Fonts
{
    using System.Collections.Generic;

    public interface IFontManager
    {
        Dictionary<Font, FontInfo> Fonts { get; }
    }
}