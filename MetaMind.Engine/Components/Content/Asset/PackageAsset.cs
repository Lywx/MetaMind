namespace MetaMind.Engine.Components.Content.Asset
{
    using System.Collections.Generic;
    using Fonts;
    using Texture;

    /// <summary>
    /// This is the package that serves as a collection of xml configuration. It 
    /// loaded a unit of gameplay session into asset manager to provide support 
    /// for asset lookup and finding.
    /// </summary>
    /// <example>
    /// The 
    /// </example>
    public class PackageAsset : Asset
    {
        public PackageAsset(string name) : base(name)
        {
        }

        public Dictionary<string, FontAsset> Fonts { get; private set; } =
            new Dictionary<string, FontAsset>();

        public Dictionary<string, ImageAsset> Texture { get; private set; } =
            new Dictionary<string, ImageAsset>();

        public Dictionary<string, ControlAsset> Controls { get; private set; } =
            new Dictionary<string, ControlAsset>();

        public void Add(FontAsset font)
        {
            this.Fonts.Add(font.Name, font);
        }

        public void Add(ImageAsset image)
        {
            this.Texture.Add(image.Name, image);
        }
    }
}
